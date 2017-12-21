import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';

/* Services */
import { LibraryService } from './library.service';

/* Components */
import { AppComponent } from './app.component';
import { BooksComponent } from './books/books.component';
import { UsersComponent } from './users/users.component';
import { BookComponent } from './books/book/book.component';
import { BookDetailsComponent } from './book-details/book-details.component';
import { UserComponent } from './users/user/user.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { UserDlgComponent } from './users/user-dlg/user-dlg.component';
import { BookDlgComponent } from './books/book-dlg/book-dlg.component';

@NgModule({
  declarations: [
    AppComponent,
    BooksComponent,
    UsersComponent,
    BookComponent,
    BookDetailsComponent,
    UserComponent,
    UserDetailsComponent,
    UserDlgComponent,
    BookDlgComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot([{
        path: '',
        redirectTo: 'books',
        pathMatch: 'full'
    }, {
        path: 'books',
        component: BooksComponent
    }, {
        path: 'users',
        component: UsersComponent
    }, {
        path: 'books/:id',
        component: BookDetailsComponent
    }, {
        path: 'users/:id',
        component: UserDetailsComponent
    }]),
    NgbModule.forRoot()
  ],
  providers: [LibraryService],
  bootstrap: [AppComponent],
  entryComponents: [UserDlgComponent, BookDlgComponent]
})
export class AppModule { }
