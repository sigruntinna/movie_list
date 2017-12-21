import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, Validators } from '@angular/forms';
import { User } from '../../library.service';

@Component({
    selector: 'app-user-dlg',
    templateUrl: './user-dlg.component.html',
    styleUrls: ['./user-dlg.component.css']
})
export class UserDlgComponent implements OnInit {

    user: User;

    constructor(public activeModal: NgbActiveModal,
                public fb: FormBuilder) { }

    ngOnInit() {
    }

    onCancel() {
        this.activeModal.dismiss();
    }

    onOk() {
        this.activeModal.close(this.user);
        location.reload();
    }

}
