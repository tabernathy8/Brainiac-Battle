import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthorizeService } from '../../../api-authorization/authorize.service';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

@Component({
  selector: 'garden',
  templateUrl: './garden.component.html',
  styleUrls: ['./garden.component.css']
})
export class GardenComponent implements OnInit {
  public isAuthenticated: Observable<boolean>;
  public userName: Observable<string>;
  private baseUrl: string;

  gameTime: number;
  gameScore: number; // percent (0-1) will be multiplied by max score

  constructor(private http: HttpClient, private authorizeService: AuthorizeService, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    class AccountDto {
      accountId: number;
      email: string;
      username: string;
      brainRating: number;
      currentGameId: number;
      totalPlayingTime: number;
      startTime: Date;
    }

    var localEmail: string;
    var account: any;

    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
    this.userName.subscribe(email => {
      localEmail = email;
      this.http.get(this.baseUrl + 'api/Account/Get/Email/' + localEmail).subscribe(a => account = a);
    });

    (window as any).submitGardenScore = (gameScore: number) => {
      this.gameScore = gameScore;
      this.saveScore(account.accountId);
    }

    (window as any).submitGardenTime = (gameTime: number) => {
      this.gameTime = gameTime;
    }
  }

  saveScore(accountId: number) {
    class ResultDto {
      accountId: number;
      categoryId: number;
      gameId: number;
      score: number;
    }

    var result = new ResultDto();
    result.accountId = accountId;
    result.categoryId = 2;
    result.gameId = 12;
    result.score = Math.floor(this.gameScore * 1000);

    this.http.post('api/Game/Add/Result', result).subscribe(result => {
    }, error => console.error(error));
  }
}
