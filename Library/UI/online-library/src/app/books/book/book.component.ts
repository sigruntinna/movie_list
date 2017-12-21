import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Book, LibraryService } from '../../library.service';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { BookDlgComponent } from '../book-dlg/book-dlg.component';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.css']
})
export class BookComponent implements OnInit {
    @Input() book: Book;
    @Output() bookUpdated = new EventEmitter();

    constructor(private modalService: NgbModal,
                private router: Router,
                private service: LibraryService) { }

    ngOnInit() { }

    onVisitBookDetails(book: Book) {
        this.router.navigate(['/books/' + book.bookId]);
    }

    deleteBookById(id: number) {
        this.service.deleteBookById(id).subscribe(result => {
            location.reload();
        });
    }

    updateBookById(id: number) {
        this.service.getBookById(id).subscribe(result => {
            this.bookUpdated.emit(false);
            const modalInstance = this.modalService.open(BookDlgComponent);
            modalInstance.componentInstance.book = result;
            modalInstance.result.then(obj => {
                console.log('Dialog was closed using OK');
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
                };
                this.service.updateBookById(id, params).subscribe(result => {
                    console.log('Updated book');
                });
            });
            this.bookUpdated.emit(this.book);
        });
    }
}
