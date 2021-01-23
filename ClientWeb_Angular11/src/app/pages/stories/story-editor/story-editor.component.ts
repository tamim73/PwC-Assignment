import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { StoriesService } from '../stories.service';

@Component({
  selector: 'app-story-editor',
  templateUrl: './story-editor.component.html',
  styleUrls: ['./story-editor.component.scss'],
})
export class StoryEditorComponent implements OnInit {
  constructor(private fb: FormBuilder, private storiesService: StoriesService) {}
  editorOptions = {
    height: 500,
    menubar: false,
    plugins: [
      'advlist autolink lists link image charmap print preview anchor',
      'searchreplace visualblocks code fullscreen',
      'insertdatetime media table paste code help wordcount',
    ],
    toolbar:
      'undo redo | formatselect | bold italic backcolor | \
    alignleft aligncenter alignright alignjustify | \
    bullist numlist outdent indent | removeformat | help',
  };

  storyFG = this.fb.group({
    title: ['', [Validators.required]],
    description: ['', [Validators.required]],
    content: ['', []],
  });

  ngOnInit(): void {
    this.storiesService.getStory(1);
  }

  save(): void {
    console.log(this.storyFG.value);  
  }
}
