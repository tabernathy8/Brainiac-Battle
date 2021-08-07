import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'garden-unity',
  templateUrl: './garden-unity.component.html',
  styleUrls: ['./garden-unity.component.css']
})
export class GardenUnityComponent implements OnInit {

  gameInstance: any;
  progress = 0;
  isReady = false;

  constructor() { }

  ngOnInit(): void {
    const loader = (window as any).UnityLoader;

    this.gameInstance = loader.instantiate(
      'gameContainerGarden',
      '/assets/GardenGame/Build/GardenGame.json', {
      onProgress: (gameInstance: any, progress: number) => {
        this.progress = progress;
        if (progress === 1) {
          this.isReady = true;
        }
      }
    });
  }

}
