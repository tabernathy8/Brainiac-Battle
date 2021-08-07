import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { gamesComponent } from './games/games.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { skillsComponent } from './skills/skills.component';
import { userComponent } from './user/user.component';
import { LeaderboardComponent } from './user/leaderboard/leaderboard.component';
import { SettingComponent } from './user/setting/setting.component';
import { FocusComponent } from './skills/focus/focus.component';
import { MemoryComponent } from './skills/memory/memory.component';
import { ReflexComponent } from './skills/reflex/reflex.component';
import { StrategyComponent } from './skills/strategy/strategy.component';
import { MeditationComponent } from './games/meditation/meditation.component';
import { TypingComponent } from './games/typing/typing.component';
import { MatchUnityComponent } from './match-unity/match-unity.component';
import { MatchComponent } from './games/match/match.component';
import { VgCoreModule } from 'videogular2/compiled/core';
import { VgControlsModule } from 'videogular2/compiled/controls';
import { VgOverlayPlayModule } from 'videogular2/compiled/overlay-play';
import { VgBufferingModule } from 'videogular2/compiled/buffering';
import { GardenUnityComponent } from './garden-unity/garden-unity.component';
import { GardenComponent } from './games/garden/garden.component';
import { ConcentrationComponent } from './games/concentration/concentration.component';
import { CupUnityComponent } from './cup-unity/cup-unity.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    gamesComponent,
    skillsComponent,
    userComponent,
    LeaderboardComponent,
    SettingComponent,
    FocusComponent,
    MemoryComponent,
    ReflexComponent,
    StrategyComponent,
    MeditationComponent,
    MatchUnityComponent,
    MatchComponent,
    GardenUnityComponent,
    GardenComponent,
    TypingComponent,
    ConcentrationComponent,
    CupUnityComponent,

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    VgCoreModule,
    VgControlsModule,
    VgOverlayPlayModule,
    VgBufferingModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
      { path: 'games', component: gamesComponent },
      { path: 'skills', component: skillsComponent, canActivate: [AuthorizeGuard]},
      { path: 'user', component: userComponent, canActivate: [AuthorizeGuard] },
      { path: 'focus', component: FocusComponent},
      { path: 'memory', component: MemoryComponent },
      { path: 'reflex', component: ReflexComponent },
      { path: 'strategy', component: StrategyComponent },
      { path: 'meditation', component: MeditationComponent },
      { path: 'typing', component: TypingComponent },
      { path: 'setting', component: SettingComponent },
      { path: 'match', component: MatchComponent },
      { path: 'garden', component: GardenComponent },
      { path: 'concentration', component: ConcentrationComponent },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
