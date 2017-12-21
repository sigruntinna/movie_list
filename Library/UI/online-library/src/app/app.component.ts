import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Rafrænt Bókasafn';

  constructor(private router: Router) { }

  onVisitBooks() {
      this.router.navigate(['/books']);
  }

  onVisitUsers() {
      this.router.navigate(['/users']);
  }
}
