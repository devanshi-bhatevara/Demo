import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DemoService } from '../../services/demo.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.css'
})
export class EditComponent implements OnInit  {

  demoForm!: FormGroup;
  constructor(
    private demoService: DemoService,
    private router: Router,
    private fb: FormBuilder,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.demoForm = this.fb.group({
      id: [0],
      name: ['',[Validators.required, Validators.minLength(2)]],
      age: [0,[Validators.required,Validators.max(100), Validators.min(18)]]
    })

    this.getDemo();

  }

  get formControl(){
    return this.demoForm.controls;
   }

   getDemo():void{
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.demoService.getById(id).subscribe({
      next: (response) => {
        if (response.success) {
          this.demoForm.patchValue({
            id: response.data.id,
            name: response.data.name,
            age: response.data.age,     
          });
        }
        else {
          console.error('Failed to fetch contacts', response.message);
        }
      },
      error: (error) => {
        alert(error.error.message);
      },
      complete: () => {
      },
    });
    }

    OnSubmit(){
  
      if(this.demoForm.valid){
        this.demoService.modify(this.demoForm.value).subscribe({
          next: (response) => {
            if (response.success) {
              this.router.navigate(['/index']);
            }
            else{
              alert(response.message)
            }
          },
          error:(err)=>{
            alert(err.error.message);
            console.log(err);
  
          },
          complete:()=>{
            console.log("Completed");
  
          }
        })
      }
    }

}
