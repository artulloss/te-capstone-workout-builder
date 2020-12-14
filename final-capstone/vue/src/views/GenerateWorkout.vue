<template>
  <v-card class="container mx-auto mt-5">
    <v-card-title>
      <h1 class="display-1">Generate Workout</h1>
    </v-card-title>
    <v-card-text>
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
        suffix="seconds"
        outlined
        label="Time"
        class="form-control"
        :rules="numericRules"
        append-outer-icon="mdi-chevron-up"
        prepend-icon="mdi-chevron-down"
        @click:append-outer="updateselectedTime(10)"
        @click:prepend="updateselectedTime(-10)"
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
      numericRules: [(v) => (v || 0) >= 0 || "Negative values are not allowed"],
    };
  },
  computed: {
    focusNames() {
      return this.focuses.map(
        (f) => f.focusName.charAt(0).toUpperCase() + f.focusName.slice(1) // Capitalize first letter :p
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
      this.selectedTime = randomNumber(10) * 60;
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
      const exercises = await this.getTrainerExercises();
      const workout = this.generateWorkout(exercises);
      this.replaceWorkout(workout);
    },
    replaceWorkout(exercises) {
      const username = this.$store.state.user.username;
      exerciseService.deleteUserExercises(username).then((response) => {
        if (response.status === 204) {
          exerciseService.addUserExercises(username, exercises).then((r) => {
            if (r.status === 201) {
              this.$router.push({ name: "exercises" });
            }
          });
        }
      });
    },
    async getTrainerExercises() {
      let exercises = [];
      for (const trainerName of this.selectedTrainers) {
        const response = await exerciseService.getTrainerExercises(trainerName);
        console.log({ response });
        if (response.status === 200) {
          console.log("DATA", response.data);
          exercises = exercises.concat(response.data);
          console.log("CONCATED", { exercises });
        }
      }
      return exercises;
    },
    generateWorkout(exercises) {
      console.log(exercises);
      let time = 0;
      const workout = [];
      while (time < this.selectedTime) {
        const exercise = exercises[randomNumber(exercises.length) - 1];
        console.log(exercise);
        time += exercise.time;
        workout.push(exercise);
      }
      return workout;
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
