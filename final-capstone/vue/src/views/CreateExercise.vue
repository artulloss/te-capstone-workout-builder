<template>
  <v-card width="400" class="mx-auto mt-5">
    <v-card-title>
      <h1 class="display-1">Create an Exercise</h1>
    </v-card-title>
    <v-card-text>
      <v-form class="form-signin" @submit.prevent="createExercise">
        <div class="alert alert-danger" role="alert" v-if="false">
          There was a problem creating the exercise.
        </div>
        <v-text-field
          id="exerciseName"
          outlined
          class="form-control"
          v-model="exercise.exerciseName"
          :rules="exerciseNameRules"
          :counter="50"
          label="Name"
          required
          autofocus
        ></v-text-field>
        <v-textarea
          id="description"
          outlined
          class="form-control"
          v-model="exercise.description"
          :rules="exerciseNameRules"
          :counter="2000"
          label="Description"
          required
        ></v-textarea>
        <v-text-field
          id="time"
          outlined
          class="form-control"
          :rules="exerciseNameRules"          
          label="Time"
          required
          v-model.number="exercise.time" 
          append-outer-icon="mdi-keyboard_arrow_up" 
          @click:append-outer="exercise.time++" 
          prepend-icon="mdi-keyboard_arrow_down" 
          @click:prepend="exercise.time--">
        >     
        <v-icon slot="append" color="red">mdi-keyboard_arrow_up</v-icon><v-icon slot="prepend" color="green">mdi-keyboard_arrow_down</v-icon>
        </v-text-field>
        <v-divider />
        <v-card-actions>
          <v-btn color="success" :to="{ name: 'register' }"
            ><span>Register</span></v-btn
          >
          <v-spacer />
          <v-btn color="info" type="submit"><span>Sign in</span></v-btn>
        </v-card-actions>
      </v-form>
    </v-card-text>
  </v-card>
</template>

<script>
import exerciseService from "../services/ExerciseService";

export default {
  name: "create-exercise",
  components: {},
  data() {
    return {
      exercise: {
          exerciseName: "",          
          description: "",
          focusId: 0,
          time: 0,
          userId: 0,
          weight: null,
          repetitions: null,
          sets: null,
          
      },
      exerciseNameRules: [
        (v) =>
          (v || "").length <= 50 || `A maximum of 50 characters is allowed`,

      ],
      
    };
  },
  methods: {
    createExercise() {
     exerciseService.postExercise(this.exercise).then(response => {
         if(response.status === 201){
            this.$router.push({name: "exercises"})
         }
         });
    },
  },
};
</script>
