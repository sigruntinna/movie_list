import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'our movie list <3';

  constructor(private router: Router) { }

  openMovies() {
    this.router.navigateByUrl('');
  }

  openAbout() {
    this.router.navigateByUrl('about')
  }
}
