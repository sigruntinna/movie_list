import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { User, LibraryService } from '../../library.service';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { UserDlgComponent } from '../user-dlg/user-dlg.component';

@Component({
    selector: 'app-user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
    @Input() user: User;
    @Output() userUpdated = new EventEmitter();

    constructor(private modalService: NgbModal,
              private router: Router,
              private service: LibraryService) { }

    ngOnInit() {
    }

    onVisitUserDetails(user: User) {
        this.router.navigate(['/users/' + user.userId]);
    }

    deleteUserById(id: number) {
        this.service.deleteUserById(id).subscribe(result => {
            location.reload();
        });
    }

    updateUserById(id: number) {
        this.service.getUserById(id).subscribe(result => {
            this.userUpdated.emit(false);
            const modalInstance = this.modalService.open(UserDlgComponent);
            modalInstance.componentInstance.user = result;
            modalInstance.result.then(obj => {
                console.log('Dialog was closed using OK');
                const params = {
                    FirstName: obj.firstName,
                    LastName: obj.lastName,
                    Address: obj.address,
                    Email: obj.email
                };
                this.service.updateUserById(id, params).subscribe(result => {
                    console.log('Updated user');
                });
            });
            this.userUpdated.emit(this.user);
        });
    }
}
