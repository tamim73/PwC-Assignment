import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AddPostRequest, AddStoryRequest } from '../stories.DTO';
import { StoriesService } from '../stories.service';

@Component({
  selector: 'app-story-editor',
  templateUrl: './story-editor.component.html',
  styleUrls: ['./story-editor.component.scss'],
})
export class StoryEditorComponent implements OnInit {
  constructor(
    private fb: FormBuilder,
    private storiesService: StoriesService,
    private route: ActivatedRoute
  ) {}

  @Input() storyId: number;
  @Input() type: 'story' | 'post';
  @Input() mode: 'add' | 'edit';

  // tinymce editor options
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

  id = +this.route.snapshot.paramMap.get('id');
  ngOnInit(): void {
    if (!this.type) {
      this.type = this.storyId ? 'post' : 'post';
    }
    if (!this.mode) {
      this.mode = this.id ? 'edit' : 'add';
    }
    if (this.mode === 'edit') {
      this.storiesService.getStory$(this.id).subscribe((res) => {});
    }
  }

  save(): void {
    console.log(this.storyFG.value);

    if (this.storyFG.invalid) {
      return;
    }

    const { title, description, content } = this.storyFG.value;

    if (this.type === 'story') {
      const req: AddStoryRequest = {
        title,
        content,
        description,
      };
      this.storiesService.addStory(req);
    }
    if (this.type === 'post') {
      const req: AddPostRequest = {
        title,
        content,
        description,
        storyId: this.storyId,
      };
      this.storiesService.addPost(req);
    }
  }
}
