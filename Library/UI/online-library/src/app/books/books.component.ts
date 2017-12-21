import { Component, OnInit } from '@angular/core';
import { LibraryService, Book } from '../library.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { AppComponent } from '../app.component';
import { BookDlgComponent } from './book-dlg/book-dlg.component';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.css']
})
export class BooksComponent implements OnInit {

    private books: Book[];
    private book: Book;

    constructor(private service: LibraryService,
                private modalService: NgbModal) { }

    ngOnInit() {
        this.service.getBooks().subscribe(result => {
            // console.log(result);
            this.books = result;
        });
    }

    addBook() {
        const modalInstance = this.modalService.open(BookDlgComponent);
        modalInstance.componentInstance.book = {};
        modalInstance.result.then(obj => {
            console.log('Dialog was closed using OK');
            console.log(obj);
            var newDate = new Date();
            if (obj.dateOfIssue.length == 10 && obj.dateOfIssue.charAt(2) == '/' && obj.dateOfIssue.charAt(5) == '/') {
                newDate = new Date(parseInt(obj.dateOfIssue.slice(6, 10)), parseInt(obj.dateOfIssue.slice(3, 5)), parseInt(obj.dateOfIssue.slice(0, 2)));
                console.log(newDate);
            }
            const params = {
                Title: obj.title,
                AuthorFirst: obj.authorFirst,
                AuthorLast: obj.authorLast,
                DateOfIssue: newDate,
                ISBNNumber: obj.isbnNumber
            }
            this.service.addBook(params).subscribe(result => {
                // this.toastrService.success('Þú hefur bætt við notanda!');
                location.reload();
            })
        });
    }
}
