import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { User, LibraryService } from '../library.service';
import { UserDlgComponent } from '../users/user-dlg/user-dlg.component';

@Component({
    selector: 'app-user-details',
    templateUrl: './user-details.component.html',
    styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
    private user: User;
    private userId: number;
    private firstName: string;
    private lastName: string;
    private address: string;
    private email: string;
    @Output() userUpdated = new EventEmitter();

    constructor(private modalService: NgbModal,
              private service: LibraryService,
              private router: Router,
              private route: ActivatedRoute) { }

    ngOnInit() {
        this.userId = this.route.snapshot.params['id'];
        this.service.getUsers().subscribe(result => {
            if (this.userId > result[result.length - 1].userId) {
                this.router.navigate(['/users']);
            }
        });
        this.service.getUserById(this.route.snapshot.params['id']).subscribe(result => {
            console.log(result);
            this.user = result;
            this.firstName = this.user.firstName;
            this.lastName = this.user.lastName;
            if (this.user.address != null) {
                this.address = this.user.address;
            } else {
                this.address = "Vantar heimilisfang.";
            }
            if (this.user.email != null) {
                this.email = this.user.email;
            } else {
                this.email = "Vantar netfang.";
            }

        });
    }

    deleteUserById(id: number) {
        this.service.deleteUserById(id).subscribe(result => {
            this.router.navigate(['/users']);
        });
    }

    updateUserById(id: number) {
        this.userUpdated.emit(false);
        const modalInstance = this.modalService.open(UserDlgComponent);
        modalInstance.componentInstance.user = this.user;
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
    }
}
