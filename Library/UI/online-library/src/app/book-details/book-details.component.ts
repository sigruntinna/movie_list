import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Book, LibraryService } from '../library.service';
import { BookDlgComponent } from '../books/book-dlg/book-dlg.component';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {
    private book: Book;
    @Output() bookUpdated = new EventEmitter();
    private bookId: number;
    private title: string;
    private authorFirst: string;
    private authorLast: string;
    private dateOfIssue: Date;
    private isbnNumber: string;
    private rating: string;

  constructor(private modalService: NgbModal,
              private service: LibraryService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit() {
      this.bookId = this.route.snapshot.params['id'];
      this.service.getBooks().subscribe(result => {
          if (this.bookId > result[result.length - 1].bookId) {
              this.router.navigate(['/books']);
          }
      });
      this.service.getBookById(this.route.snapshot.params['id']).subscribe(result => {
          console.log(result);
          this.book = result;
          this.title = this.book.title;
          this.authorFirst = this.book.authorFirst;
          this.authorLast = this.book.authorLast;
          this.dateOfIssue = this.book.dateOfIssue;
          this.isbnNumber = this.book.isbnNumber;
          this.rating = this.book.rating;
      });
  }

  deleteBookById(id: number) {
      this.service.deleteBookById(id).subscribe(result => {
          this.router.navigate(['/books']);
      });
  }

  updateBookById(id: number) {
      this.bookUpdated.emit(false);
      const modalInstance = this.modalService.open(BookDlgComponent);
      modalInstance.componentInstance.book = this.book;
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
  }
}
