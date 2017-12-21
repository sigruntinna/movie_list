import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, Validators } from '@angular/forms';
import { Book } from '../../library.service';

@Component({
  selector: 'app-book-dlg',
  templateUrl: './book-dlg.component.html',
  styleUrls: ['./book-dlg.component.css']
})
export class BookDlgComponent implements OnInit {

    book: Book;

    constructor(public activeModal: NgbActiveModal,
                public fb: FormBuilder) { }

    ngOnInit() {
    }

    onCancel() {
      this.activeModal.dismiss();
    }

    onOk() {
      this.activeModal.close(this.book);
      location.reload();
    }

}
