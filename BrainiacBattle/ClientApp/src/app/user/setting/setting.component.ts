import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthorizeService } from '../../../api-authorization/authorize.service';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
@Component({
  selector: 'app-setting',
  templateUrl: './setting.component.html',
  styleUrls: ['./setting.component.css']
})
export class SettingComponent implements OnInit {
  uname;
  formdata;
  account;
  public isAuthenticated: Observable<boolean>;
  public userName: Observable<string>;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private authorizeService: AuthorizeService) {

  }
  ngOnInit() {

    this.formdata = new FormGroup({
      uname: new FormControl("")
    });
    var localEmail: string;
    var accounts: string;

    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
    this.userName.subscribe(email => localEmail = email);

    this.http.get('api/Account/Get/Email/' + localEmail).subscribe(a => this.account = a);
  }
  onClickSubmit(data) {
    this.uname = data.uname;
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'application/json');
    var postData = {
      accountId: this.account.accountId,
      username: this.uname
    };
    this.http.post('api/Account/ChangeUsername/' + this.account.accountId + '/' + this.uname, postData, { headers: headers })
      .subscribe(error => console.error(error));
  }
}