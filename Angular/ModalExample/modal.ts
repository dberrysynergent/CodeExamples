import { Component, OnInit } from '@angular/core';
import { EmailContact } from 'src/app/models/emailContact';

@Component({
  selector: 'app-manage-contacts',
  templateUrl: './manage-contacts.component.html',
  styleUrls: ['./manage-contacts.component.scss']
})
export class ManageContactsComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    console.log(this.contactList);
  }

  save() {

  }

  close() {

  }

}