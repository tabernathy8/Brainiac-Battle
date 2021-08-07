import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthorizeService } from '../../../api-authorization/authorize.service';
import { Observable, PartialObserver } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { Users } from '../user.model';
@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.css']
})
export class LeaderboardComponent implements OnInit {
  public name: Observable<string>;
  private baseUrl: string;
  public users: Users[];
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { this.baseUrl = baseUrl; }

  ngOnInit() {
    class LeaderBoardDto {
      rank: number;
      name: string;
      score: number;
    }

    var board: PartialObserver<LeaderBoardDto>;
    
    this.http.get(this.baseUrl + 'Api/Account/Get/Leaderboard/10').subscribe(leaderBoard => {
      console.log(leaderBoard[0].name);
      this.users = [
      new Users(leaderBoard[0].name, leaderBoard[0].score),
        new Users(leaderBoard[1].name, leaderBoard[1].score),
        new Users(leaderBoard[2].name, leaderBoard[2].score),
        new Users(leaderBoard[3].name, leaderBoard[3].score),
        new Users(leaderBoard[4].name, leaderBoard[4].score),
        new Users(leaderBoard[5].name, leaderBoard[5].score),
        new Users(leaderBoard[6].name, leaderBoard[6].score),
        new Users(leaderBoard[7].name, leaderBoard[7].score),
        new Users(leaderBoard[8].name, leaderBoard[8].score),
        new Users(leaderBoard[9].name, leaderBoard[9].score),
      ];

    });


    

  }

  

}
