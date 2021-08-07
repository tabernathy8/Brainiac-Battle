import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'cup-unity',
  templateUrl: './cup-unity.component.html',
  styleUrls: ['./cup-unity.component.css']
})
export class CupUnityComponent implements OnInit {

  gameInstance: any;
  progress = 0;
  isReady = false;

  constructor() { }

  ngOnInit(): void {
    const loader = (window as any).UnityLoader;

    this.gameInstance = loader.instantiate(
      'gameContainerCup',
      '/assets/CupGame/Build/CupGame.json', {
      onProgress: (gameInstance: any, progress: number) => {
        this.progress = progress;
        if (progress === 1) {
          this.isReady = true;
        }
      }
    });
  }

}
