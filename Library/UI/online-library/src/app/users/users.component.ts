import { Component, OnInit } from '@angular/core';
import { User, LibraryService } from '../library.service';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserDlgComponent } from './user-dlg/user-dlg.component';


@Component({
    selector: 'app-users',
    templateUrl: './users.component.html',
    styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
    private users: User[];
    private user: User;

    constructor(private service: LibraryService,
                private modalService: NgbModal) { }

    ngOnInit() {
        this.service.getUsers().subscribe(result => {
        //   console.log(result);
            this.users = result;
        });
    }

    addUser() {
        const modalInstance = this.modalService.open(UserDlgComponent);
        modalInstance.componentInstance.user = {};
        modalInstance.result.then(obj => {
            console.log('Dialog was closed using OK');
            const params = {
                FirstName: obj.firstName,
                LastName: obj.lastName,
                Address: obj.address,
                Email: obj.email
            }
            this.service.addUser(params).subscribe(result => {
                location.reload();
            });
        });
    }
}
