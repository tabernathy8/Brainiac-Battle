import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthorizeService } from '../../../api-authorization/authorize.service';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

@Component({
  selector: 'concentration',
  templateUrl: './concentration.component.html',
  styleUrls: ['./concentration.component.css']
})
export class ConcentrationComponent implements OnInit {
  public isAuthenticated: Observable<boolean>;
  public userName: Observable<string>;
  private baseUrl: string;

  gameScore: number;

  constructor(private http: HttpClient, private authorizeService: AuthorizeService, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    var localEmail: string;
    var account: any;

    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
    this.userName.subscribe(email => {
      localEmail = email;
      this.http.get(this.baseUrl + 'api/Account/Get/Email/' + localEmail).subscribe(a => account = a);
    });

    (window as any).submitCupScore = (gameScore: number) => {
      this.gameScore = gameScore;
      this.saveScore(account.accountId);
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
    result.categoryId = 3;
    result.gameId = 13;
    result.score = Math.floor(this.gameScore);

    this.http.post('api/Game/Add/Result', result).subscribe(result => {
    }, error => console.error(error));
  }
}
