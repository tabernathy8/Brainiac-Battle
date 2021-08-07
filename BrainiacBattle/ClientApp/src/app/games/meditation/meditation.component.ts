import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component,Inject, OnInit } from '@angular/core';
import { AuthorizeService } from '../../../api-authorization/authorize.service';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
@Component({
  selector: 'app-meditation',
  templateUrl: './meditation.component.html',
  styleUrls: ['./meditation.component.css']
})
export class MeditationComponent implements OnInit {
  public isAuthenticated: Observable<boolean>;
  public userName: Observable<string>;
  private baseUrl: string;
  public interval;
  public startTime = 0;
  account;
  playlist = [
    {
      title: 'Music, Body and Spirit',
      src: "assets/videos/Body.mp4",
      type: 'video/mp4'
    },
    {
      title: 'Guided Meditation',
      src: "assets/videos/Meditation.mp4",
      type: 'video/mp4'
    },
    {
      title: 'Meditation With Rain',
      src: "assets/videos/rain.mp4",
      type: 'video/mp4'
    },
    {
      title: 'Yoga Music',
      src: "assets/videos/yoga.mp4",
      type: 'video/mp4'
    },
    {
      title: 'Wave Meditation',
      src: "assets/videos/wave.mp4",
      type: 'video/mp4'
    }
  ];
  currentIndex = 0;
  currentItem = this.playlist[this.currentIndex];
  api;
  constructor(private http: HttpClient, private authorizeService: AuthorizeService, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    var localEmail: string;

    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
    this.userName.subscribe(email => {
      localEmail = email;
      this.http.get(this.baseUrl + 'api/Account/Get/Email/' + localEmail).subscribe(a => this.account = a);
    });
  }
  
  onPlayerReady(api) {
    this.api = api;
    //this.api.getDefaultMedia().subscriptions.loadedMetadata.subscribe(this.playVideo.bind(this));
    this.api.getDefaultMedia().subscriptions.ended.subscribe(this.nextVideo.bind(this));
  }

  nextVideo() {
    this.currentIndex++;
    if (this.currentIndex === this.playlist.length) {
      this.currentIndex = 0;
    }
    this.currentItem = this.playlist[this.currentIndex];
  }

  playVideo() {
    this.api.play();
  }

  onClickPlaylistItem(item, index: number) {
    this.currentIndex = index;
    this.currentItem = item;
  }

 
play(){
  this.interval = setInterval(() => {
    this.startTime++;
  }, 1000);
}

  pause() {
    clearInterval(this.interval);
  }

saveScore(){
  class ResultDto {
    AccountId: number;
    CategoryId: number;
    GameId: number;
    Score: number;
  }
  let headers = new HttpHeaders();
  headers.append('Content-Type', 'application/json');
  console.log(this.account.accountId);
  let result = new ResultDto();
  result.AccountId = Math.floor(this.account.accountId);
  result.CategoryId = Math.floor(6);
  result.GameId = Math.floor(15);
  result.Score = Math.floor(this.startTime);
  console.log("posting");
  const resultBlob = new Blob([JSON.stringify(result)], { type: 'application/json' }); 

  this.http.post('api/Game/Add/Result', resultBlob).subscribe(result => {
    }, error => console.error(error));
}


  

}
