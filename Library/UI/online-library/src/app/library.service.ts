import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/Rx';

export class Book {
    bookId: number;
    title: string;
    authorFirst: string;
    authorLast: string;
    dateOfIssue: Date;
    isbnNumber: string;
    rating: string;
}

export class User {
    userId: number;
    firstName: string;
    lastName: string;
    address: string;
    email: string;
}

export class Review {
    bookReviewId: number;
    userName: string;
    dateOfReview: string;
    rating: number;
    review: string;
}

export class BooksReview {
    bookId: number;
    bookTitle: string;
    reviews: Review[];
}

export class Recommendation {
    bookId: number;
    title: string;
    authorFullName: string;
    rating: string;
}

@Injectable()
export class LibraryService {

    private headers = new Headers({'Content-Type': 'application/json'});

    constructor(private http: Http) { }

    /*---------- GET SERVICE FUNCTIONS -----------*/

    getBooks(): Observable<Book[]> {
        return this.http.get('http://localhost:51145/api/books')
        .map(response => {
            return <Book[]> response.json();
        });
    }

    getUsers(): Observable<User[]> {
        return this.http.get('http://localhost:51145/api/users')
        .map(response => {
            return <User[]> response.json();
        });
    }

    getReviews(): Observable<BooksReview[]> {
        return this.http.get('http://localhost:51145/api/books/reviews')
        .map(response => {
            return <BooksReview[]> response.json();
        });
    }

    getBookById(id: number): Observable<Book> {
        this.getBooks().subscribe(result => {
            return null;
        });
        return this.http.get('http://localhost:51145/api/books/' + id)
        .map(response => {
            return <Book> response.json();
        });
    }

    getUserById(id: number): Observable<User> {
        this.getUsers().subscribe(result => {
            return null;
        });
        return this.http.get('http://localhost:51145/api/users/' + id)
        .map(response => {
            return <User> response.json();
        });
    }

    getBooksByUserId(id: number): Observable<Book[]> {
        return this.http.get('http://localhost:51145/api/users/' + id + '/books')
        .map(response => {
            return <Book[]> response.json();
        });
    }

    getReviewsByUserId(id: number): Observable<Review[]> {
        return this.http.get('http://localhost:51145/api/users/' + id + '/reviews')
        .map(response => {
            return <Review[]> response.json();
        });
    }

    getReviewsByBookId(id: number): Observable<Review[]> {
        return this.http.get('http://localhost:51145/api/books/' + id + '/reviews')
        .map(response => {
            return <Review[]> response.json();
        });
    }

    getReviewsByBookIdAndByUserId(bookId: number, userId: number): Observable<Review[]> {
        return this.http.get('http://localhost:51145/api/books/' + bookId + '/reviews/' + userId)
        .map(response => {
            return <Review[]> response.json();
        });
    }

    getRecommendationsByUserId(id: number): Observable<Recommendation[]> {
        return this.http.get('http://localhost:51145/api/users/' + id + '/recommendation')
        .map(response => {
            return <Recommendation[]> response.json();
        });
    }

    /*---------- POST SERVICE FUNCTIONS -----------*/

    addBook(obj: any): Observable<any> {
        return this.http.post('http://localhost:51145/api/books', obj);
    }

    addUser(obj: any): Observable<any> {
        return this.http.post('http://localhost:51145/api/users', obj);
    }

    addBookByIdToUserById(obj: any, userId: number, bookId: number): Observable<any> {
        return this.http.post('http://localhost:51145/api/users/' + userId + '/books/' + bookId, obj);
    }

    addReviewByBookIdToUserById(obj: any, userId: number, bookId: number): Observable<any> {
        return this.http.post('http://localhost:51145/api/users/' + userId + '/reviews/' + bookId, obj);
    }

    /*---------- PUT SERVICE FUNCTIONS -----------*/

    updateBookById(id: number, obj: any): Observable<any> {
        return this.http.put('http://localhost:51145/api/books/' + id, obj);
    }

    updateUserById(id: number, obj: any): Observable<any> {
        return this.http.put('http://localhost:51145/api/users/' + id, obj);
    }

    updateReviewByBookIdByUserId(userId: number, bookId:number, obj: any): Observable<any> {
        return this.http.put('http://localhost:51145/api/users/' + userId + '/reviews/' + bookId, obj);
    }

    updateReviewByUserIdByBookId(userId: number, bookId:number, obj: any): Observable<any> {
        return this.http.put('http://localhost:51145/api/books/' + bookId + '/reviews/' + userId, obj);
    }

    /*---------- DELETE SERVICE FUNCTIONS -----------*/

    deleteBookById(id: number): Observable<any> {
        return this.http.delete('http://localhost:51145/api/books/' + id);
    }

    deleteUserById(id: number): Observable<any> {
        return this.http.delete('http://localhost:51145/api/users/' + id);
    }

    deleteBookByIdFromUserById(userId: number, bookId:number): Observable<any> {
        return this.http.delete('http://localhost:51145/api/users/' + userId + '/books/' + bookId);
    }

    deleteReviewByBookIdFromUserById(userId: number, bookId:number): Observable<any> {
        return this.http.delete('http://localhost:51145/api/users/' + userId + '/reviews/' + bookId);
    }

    deleteReviewByUserIdFromBookById(userId: number, bookId:number): Observable<any> {
        return this.http.delete('http://localhost:51145/api/book/' + bookId + '/users/' + userId);
    }
}
