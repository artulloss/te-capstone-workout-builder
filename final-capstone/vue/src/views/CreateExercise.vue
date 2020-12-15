<template>
  <v-card class="container mx-auto mt-5">
    <v-card-title>
      <h1 class="display-1">Create an Exercise</h1>
    </v-card-title>
    <v-card-text>
      <v-form class="form-signin" @submit.prevent="createExercise">
        <div class="alert alert-danger" role="alert" v-if="errorMessage !== ''">
          {{ errorMessage }}
        </div>
        <v-text-field
          id="exercise-name"
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
          :counter="2000"
          label="Description"
          required
        ></v-textarea>
        <v-select
          id="focus"
          outlined
          class="form-control"
          label="Focus"
          :items="focusNames"
          v-model="pickedFocusName"
          required
        />
        <v-text-field
          id="time"
          suffix="seconds"
          outlined
          label="Time"
          v-model.number="exercise.time"
          required
          class="form-control"
          :rules="requiredNumericRules"
          append-outer-icon="mdi-chevron-up"
          prepend-icon="mdi-chevron-down"
          @click:append-outer="exercise.time = Number(exercise.time) + 10"
          @click:prepend="exercise.time -= 10"
        />
        <v-text-field
          id="weight"
          suffix="lbs"
          outlined
          class="form-control"
          :rules="numericRules"
          label="Weight"
          v-model.number="exercise.weight"
          append-outer-icon="mdi-chevron-up"
          prepend-icon="mdi-chevron-down"
          @click:append-outer="exercise.weight++"
          @click:prepend="exercise.weight--"
        />
        <v-text-field
          id="repetitions"
          outlined
          class="form-control"
          :rules="numericRules"
          label="Repetitions"
          v-model.number="exercise.repetitions"
          append-outer-icon="mdi-chevron-up"
          prepend-icon="mdi-chevron-down"
          @click:append-outer="exercise.repetitions++"
          @click:prepend="exercise.repetitions--"
        />
        <v-text-field
          id="sets"
          outlined
          class="form-control"
          :rules="numericRules"
          label="Sets"
          v-model.number="exercise.sets"
          append-outer-icon="mdi-chevron-up"
          prepend-icon="mdi-chevron-down"
          @click:append-outer="exercise.sets++"
          @click:prepend="exercise.sets--"
        />
        <p id="error-msg"></p>
        <v-divider />
        <v-card-actions>
          <v-spacer />
          <v-btn color="info" type="submit"><span>Submit</span></v-btn>
        </v-card-actions>
      </v-form>
    </v-card-text>
  </v-card>
</template>

<script>
import exerciseService from "@/services/ExerciseService";
import focusService from "@/services/FocusService";

export default {
  name: "create-exercise",
  created() {
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
  data() {
    return {
      exercise: {
        exerciseName: "",
        description: "",
        focusId: null,
        time: null,
        userId: this.$store.state.user.userId,
        weight: null,
        repetitions: null,
        sets: null,
      },
      exerciseNameRules: [
        (v) =>
          (v || "").length <= 50 || "A maximum of 50 characters is allowed",
      ],
      numericRules: [(v) => (v || 0) >= 0 || "Negative values are not allowed"],

      focuses: [],
      pickedFocusName: "",
      errorMessage: "",
    };
  },
  computed: {
    focusNames() {
      return this.focuses.map(
        (f) => f.focusName.charAt(0).toUpperCase() + f.focusName.slice(1) // Capitalize first letter :p
      );
    },
    requiredNumericRules() {
      return (
        this.numericRules.concat((v) => (v || "").length !== 0) ||
        "This is a required field"
      );
    },
  },
  methods: {
    createExercise() {
      let arrayWithFocusObj = this.focuses.filter((f) => {
        console.log(f.focusName, this.pickedFocusName.toLowerCase());
        return f.focusName === this.pickedFocusName.toLowerCase();
      });
      const exercise = this.exercise;
      let field;
      console.log("Exercise", exercise.name, !exercise.name);

      if (!exercise.exerciseName) {
        field = "Name";
      } else if (!exercise.description) {
        field = "Description";
      } else if (!arrayWithFocusObj[0]) {
        field = "Focus";
      } else if (exercise !== 0 && !exercise.time) {
        field = "Time";
      }

      if (field) {
        this.errorMessage = `${field} is required!`;
        return;
      }
      const greaterThanZero = (value) =>
        (this.errorMessage = value + " must be greater than 0");
      if (exercise.time <= 0) {
        return greaterThanZero("Time");
      }
      if (exercise.weight === 0 || exercise.weight < 0) {
        return greaterThanZero("Weight");
      }
      if (exercise.repetitions === 0 || exercise.repetitions < 0) {
        return greaterThanZero("Repetitions");
      }
      if (exercise.sets === 0 || exercise.sets < 0) {
        return greaterThanZero("Sets");
      }

      this.errorMessage = "";
      exercise.focusId = arrayWithFocusObj[0].focusId; // This should *always* work
      exerciseService
        .postExercise(exercise)
        .then((response) => {
          console.log(response.status);
          if (response.status === 201) {
            this.$router.push({ name: "admin-exercises" });
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
};
</script>
