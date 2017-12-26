import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.scss']
})
export class MoviesComponent implements OnInit {

  itemCount: number;
  movieText: string;
  movieLink: string;
  movies = [];

  constructor() { }

  ngOnInit() {
    this.itemCount = this.movies.length;
    this.movies.push(["Get Out (2017)", "http://www.imdb.com/title/tt5052448/"],
                     ["Lady Bird (2017)", "http://www.imdb.com/title/tt4925292/"],
                     ["The Big Sick (2017)", "http://www.imdb.com/title/tt5462602/"],
                     ["Coco (2017)", "http://www.imdb.com/title/tt2380307/"],
                     ["The Room (2003)", "http://www.imdb.com/title/tt0368226/"],
                     ["The Disaster Artist (2017)", "http://www.imdb.com/title/tt3521126/?ref_=tt_rec_tt"])
  }

  addMovie() {
    this.movies.push([this.movieText, this.movieLink]);
    this.movieText = '';
    this.movieLink = '';
    this.itemCount = this.movies.length;
  }

}
