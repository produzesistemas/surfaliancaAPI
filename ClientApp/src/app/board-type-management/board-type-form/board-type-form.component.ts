import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { BoardType } from 'src/app/_models/board-type-model';
import { BoardTypeService } from 'src/app/_services/board-type.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-board-type-form',
  templateUrl: './board-type-form.component.html'
})
export class BoardTypeFormComponent implements OnInit {
    formAdd: FormGroup;
  submitted = false;
  public boardType: BoardType = new BoardType();

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private boardTypeService: BoardTypeService
  ) { }

  get q() { return this.formAdd.controls; }

  ngOnInit() {
    this.formAdd = this.formBuilder.group({
      id: [0],
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(255)]],
    });

    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.boardType.id = Number(params.id);
        this.load();
      }
    });


    }

    load() {
      if (this.boardType.id > 0) {
        this.boardTypeService.getById(this.boardType.id).subscribe(result => {
          this.boardType = result;
          this.formAdd.controls.id.setValue(this.boardType.id);
          this.formAdd.controls.name.setValue(this.boardType.name);
            });
      }

    }

    onSave() {
      this.submitted = true;
      if (this.formAdd.invalid) {
        return;
      }
      const model = new BoardType(this.formAdd.value);
      this.boardTypeService.save(model).subscribe(result => {
        this.toastr.success('Registro efetuado com sucesso!');
        this.router.navigate(['partner-area/board-type']);
    });
    }

    onCancel() {
        this.router.navigate(['partner-area/board-type']);
    }

}

