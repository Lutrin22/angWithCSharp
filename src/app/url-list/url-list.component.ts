// url-list.component.ts

import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../auth.service';

interface UrlData {
  id: number;
  shortUrl: string;
  longUrl: string;
}

@Component({
  selector: 'app-url-list',
  templateUrl: './url-list.component.html',
  styleUrls: ['./url-list.component.css']
})
export class UrlListComponent implements OnInit {
  public urls: UrlData[] = [];

  constructor(private http: HttpClient, private authService: AuthService) {}

  ngOnInit(): void {
    this.fetchUrls();
  }

  private fetchUrls(): void {
    const token = this.authService.getToken();
    if (token) {
      this.http.get<UrlData[]>('http://localhost:5000/url', {
        headers: {
          Authorization: `Bearer ${token}`
        }
      }).subscribe(
        (response) => {
          this.urls = response;
        },
        (error) => {
          console.error('Failed to fetch URLs', error);
        }
      );
    }
  }
}
