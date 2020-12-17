<template>
  <v-card class="container mx-auto mt-5">
    <v-card-title>
      <h1 class="display-1">Generate Workout</h1>
    </v-card-title>
    <v-card-text>
      <div class="alert alert-danger" role="alert" v-if="errorMessage">
        {{ errorMessage }}
      </div>
      <v-select
        id="trainers"
        outlined
        class="form-control"
        label="Trainers"
        :items="trainers"
        v-model="selectedTrainers"
        clearable
        multiple
        chips
        deletable-chips
        required
      />
      <v-select
        id="focuses"
        outlined
        class="form-control"
        label="Focuses"
        :items="focusNames"
        v-model="selectedFocuses"
        clearable
        multiple
        chips
        deletable-chips
        required
      />
      <v-text-field
        id="time"
        suffix="minutes"
        outlined
        label="Time"
        class="form-control"
        :rules="numericRules"
        append-outer-icon="mdi-chevron-up"
        prepend-icon="mdi-chevron-down"
        @click:append-outer="updateselectedTime(5)"
        @click:prepend="updateselectedTime(-5)"
        v-model.number="selectedTime"
      />
    </v-card-text>
    <v-divider />
    <v-card-actions>
      <v-form @submit.prevent="onGenerate">
        <v-btn color="success" @click="feelingLucky"
          ><span>I'm Feeling Lucky</span></v-btn
        >

        <v-btn color="info" type="submit"><span>Generate</span></v-btn>
      </v-form>
    </v-card-actions>
  </v-card>
</template>

<script>
import focusService from "@/services/FocusService";
import exerciseService from "@/services/ExerciseService";
import axios from "axios";
import utilities from "@/utilities";

const randomNumber = (max) => Math.floor(Math.random() * max) + 1;

export default {
  name: "generate-workout",
  created() {
    this.getTrainers();
    this.getFocuses();
    //this.getExercises();
  },
  data() {
    return {
      exercises: [],
      selectedTrainers: [],
      selectedFocuses: [],
      selectedTime: null,
      trainers: [],
      focuses: [],
      numericRules: utilities.numericRules,
      errorMessage: "",
    };
  },
  computed: {
    focusNames() {
      return this.focuses.map((f) =>
        utilities.capitalizeFirstLetter(f.focusName)
      );
    },
  },
  methods: {
    feelingLucky() {
      const randomArrayValues = (array) => {
        const arrayValues = [];
        for (let i = 0; i <= randomNumber(array.length) - 1; i++) {
          const index = randomNumber(array.length) - 1;
          arrayValues.push(array[index]);
        }
        return Array.from(new Set(arrayValues));
      };
      this.selectedTrainers = randomArrayValues(this.trainers);
      this.selectedFocuses = randomArrayValues(this.focusNames);
      this.selectedTime = randomNumber(12) * 5;
    },
    getFocusId() {
      let arrayWithFocusObj = this.focuses.filter((f) => {
        return f.focusName === (this.focusFilter || "").toLowerCase();
      });
      return (arrayWithFocusObj[0] || { focusId: undefined }).focusId;
    },
    getTrainers() {
      axios
        .get("/trainer")
        .then((response) => {
          if (response.status === 200) {
            this.trainers = response.data;
          }
        })
        .catch((e) => {
          console.log(e);
        });
    },
    getFocuses() {
      // Get all the focuses
      focusService
        .getFocuses()
        .then((response) => {
          if (response.status === 200) {
            this.focuses = response.data;
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    updateselectedTime(amount) {
      let newTime;
      newTime = Number(this.selectedTime) + amount;
      this.selectedTime = newTime === 0 ? "" : newTime;
      //this.updateUrl(); // TODO Maybe
    },
    async onGenerate() {
      if (!this.validateWorkoutSelections()) return;
      const exercises = await this.getTrainerExercises();
      const workout = this.generateWorkout(exercises);
      this.replaceWorkout(workout);
    },
    replaceWorkout(exercises) {
      const username = this.$store.state.user.username;
      exerciseService.deleteUserExercises(username).then((response) => {
        if (response.status === 204) {
          exerciseService
            .addUserExercises(username, exercises)
            .then((r) => {
              if (r.status === 201) {
                this.$router.push({ name: "exercises" });
              }
            })
            .catch(() => (this.errorMessage = "Failed to generate a workout!"));
        }
      });
    },
    async getTrainerExercises() {
      let exercises = [];
      for (const trainerName of this.selectedTrainers) {
        const response = await exerciseService.getTrainerExercises(trainerName);
        if (response.status === 200) {
          exercises = exercises.concat(response.data);
        }
      }
      return exercises;
    },
    generateWorkout(exercises) {
      const randomArrayValue = (array) => array[randomNumber(array.length) - 1];
      let time = 0;
      const workout = [];
      const selectedTimeSeconds = 60 * this.selectedTime;
      while (time < selectedTimeSeconds) {
        let exercise = randomArrayValue(exercises);
        let i = 0;
        while (time + exercise.time > selectedTimeSeconds) {
          exercise = randomArrayValue(exercises);
          if (++i > 1000) {
            return workout;
          }
        }
        time += exercise.time;
        workout.push(exercise);
      }
      return workout;
    },
    validateWorkoutSelections() {
      this.errorMessage = "";
      if (this.selectedTrainers.length < 1) {
        this.errorMessage = "You must select at least one trainer!";
      } else if (this.selectedFocuses.length < 1) {
        this.errorMessage = "You must select at least one focus!";
      } else if (this.selectedTime <= 0) {
        this.errorMessage = "You must select a time greater than 0";
      }
      return this.errorMessage === "";
    },
  },
};
</script>

<style scoped>
form {
  width: 100%;
  display: flex;
  justify-content: space-between;
}
</style>
