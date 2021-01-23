import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GuestGuard } from './core/guards/guest.guard';
import { PagesComponent } from './pages/pages.component';
import { StoriesListComponent } from './pages/stories/stories-list/stories-list.component';
import { StoryEditorComponent } from './pages/stories/story-editor/story-editor.component';
import { StoryViewComponent } from './pages/stories/story-view/story-view.component';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () =>
      import('src/app/auth/auth.module').then((m) => m.AuthModule),
    canActivate: [GuestGuard],
  },
  {
    path: '',
    component: PagesComponent,
    children: [
      {
        path: 'stories',
        component: StoriesListComponent
      },
      {
        path: 'stories/add',
        component: StoryEditorComponent
      },
      {
        path: 'stories/edit/:id',
        component: StoryEditorComponent
      },
      {
        path: 'stories/:id',
        component: StoryViewComponent
      },
      {
        path: '**',
        redirectTo: 'stories',
      }
    ],
  },

  // ----------
  {
    path: '**',
    redirectTo: '',
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
