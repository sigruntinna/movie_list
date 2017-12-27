import { Component, OnInit } from '@angular/core';
import { trigger, style, transition, animate, keyframes, query, stagger } from '@angular/animations';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.scss'],
  animations: [
    trigger('movies', [
      transition('* => *', [
        query(':enter', style({ opacity: 0}), {optional: true}),
        query(':enter', stagger('300ms', [
          animate('.6s ease-in', keyframes([
            style({opacity: 0, transform: 'translateY(-75%)', offset: 0}),
            style({opacity: .5, transform: 'translateY(35%)', offset: .3}),
            style({opacity: 1, transform: 'translateY(0)', offset: 1})
          ]))
        ]), {optional: true}),
        query(':leave', stagger('300ms', [
          animate('.6s ease-in', keyframes([
            style({opacity: 1, transform: 'translateY(0)', offset: 0}),
            style({opacity: .5, transform: 'translateY(35%)', offset: .3}),
            style({opacity: 0, transform: 'translateY(-75%)', offset: 1})
          ]))
        ]), {optional: true})
      ])
    ])
  ]
})

export class MoviesComponent implements OnInit {

  itemCount: number;
  movieText: string;
  movieLink: string;
  movies = [];

  constructor() { }

  ngOnInit() {
    this.movies.push(["Get Out (2017)", "http://www.imdb.com/title/tt5052448/"],
                     ["Lady Bird (2017)", "http://www.imdb.com/title/tt4925292/"],
                     ["The Big Sick (2017)", "http://www.imdb.com/title/tt5462602/"],
                     ["Coco (2017)", "http://www.imdb.com/title/tt2380307/"],
                     ["The Room (2003)", "http://www.imdb.com/title/tt0368226/"],
                     ["The Disaster Artist (2017)", "http://www.imdb.com/title/tt3521126/?ref_=tt_rec_tt"]);
    this.itemCount = this.movies.length;
  }

  addMovie() {
    this.movies.push([this.movieText, this.movieLink]);
    this.movieText = '';
    this.movieLink = '';
    this.itemCount = this.movies.length;
  }

  removeMovie(movie) {
    this.movies.splice(movie, 1);
  }

}
