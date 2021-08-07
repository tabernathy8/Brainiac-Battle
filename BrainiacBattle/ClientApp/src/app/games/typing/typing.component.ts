import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
@Component({
  selector: 'app-typing',
  templateUrl: './typing.component.html',
  styleUrls: ['./typing.component.css']
})
export class TypingComponent implements OnInit {
  name = 'Set iframe source';
  url: string = "typingFrame.html";
  urlSafe: SafeResourceUrl;
    html: any;
  constructor(public sanitizer: DomSanitizer) {}
  ngOnInit() {
    this.html = this.sanitizer.bypassSecurityTrustHtml('<iframe class = ".typing" src = "../../assets/Typing/typingFrame.html" style="height: 750px; width: 100%;"></iframe>')
  }

}
