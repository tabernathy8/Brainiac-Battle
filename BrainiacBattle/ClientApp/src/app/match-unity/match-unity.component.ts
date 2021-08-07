import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'match-unity',
  templateUrl: './match-unity.component.html',
  styleUrls: ['./match-unity.component.css']
})

export class MatchUnityComponent implements OnInit {

  gameInstance: any;
  progress = 0;
  isReady = false;

  constructor() { }

  ngOnInit(): void {
    const loader = (window as any).UnityLoader;

    this.gameInstance = loader.instantiate(
      'gameContainerMatch',
      '/assets/MatchMemory/Build/MatchMemory.json', {
      onProgress: (gameInstance: any, progress: number) => {
        this.progress = progress;
        if (progress === 1) {
          this.isReady = true;
        }
      }
    });
  }

}
