var doc = CurrentUserDoc;
if (doc != null)
{
	var root = doc.Root;
	var accountWarningCodes = root.XPathSelectElements("//ACCOUNTLIST/ACCOUNT[NUMBER={0}]/*".FormatWith(userModel.Number))
		.Where(x => x.Name.LocalName.Matches("^WARNINGCODE"))
		.Select(x => x.Value.ToInteger())
		.Where(x => x > 0).ToList();
	var accountWarningExpirations = root.XPathSelectElements("//ACCOUNTLIST/ACCOUNT[NUMBER={0}]/*".FormatWith(userModel.Number)).Where(x => x.Name.LocalName.Matches("^WARNINGEXPIRATION")).Select(x => x.Value.ToDateTimeOrBlank()).ToList();
	var blockDebitCardOverdraftWarnings = PropertyModelCache.GetCustomString1().SplitComma().Select(x => x.ToInteger()).Where(x => x != 0).ToList();

	var index = -1;
	foreach (var accountWarningCode in accountWarningCodes)
	{
		index++;

		var warningExpiration = accountWarningExpirations[index];

		if (blockDebitCardOverdraftWarnings.Contains(accountWarningCode) && (warningExpiration.IsBlank() || warningExpiration > DateTime.Now))
		{
			TraceTools.TraceData(TracePrefix, "Debit Card Overdraft Blocked (Warning)", "{0}:{1}".FormatWith(accountWarningCode, warningExpiration));

			RedirectToAction("Block");
		}
	}
}