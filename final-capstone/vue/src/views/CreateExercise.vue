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
          :counter="2000"
          label="Description"
          required
        ></v-textarea>
        <v-combobox
          id="focus"
          outlined
          class="form-control"
          :rules="focusRules"
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
        >
          <v-icon slot="append" @click="exercise.time++">
            mdi-chevron-up
          </v-icon>
          <v-icon slot="prepend" @click="exercise.time--">
            mdi-chevron-down
          </v-icon>
        </v-text-field>
        <v-text-field
          id="weight"
          suffix="lbs"
          outlined
          class="form-control"
          :rules="numericRules"
          label="Weight"
          v-model.number="exercise.weight"
        >
          <v-icon slot="append" @click="exercise.weight++">
            mdi-chevron-up
          </v-icon>
          <v-icon slot="prepend" @click="exercise.weight--">
            mdi-chevron-down
          </v-icon>
        </v-text-field>
        <v-text-field
          id="repetitions"
          outlined
          class="form-control"
          :rules="numericRules"
          label="Repetitions"
          v-model.number="exercise.repetitions"
        >
          <v-icon slot="append" @click="exercise.repetitions++">
            mdi-chevron-up
          </v-icon>
          <v-icon slot="prepend" @click="exercise.repetitions--">
            mdi-chevron-down
          </v-icon>
        </v-text-field>
        <v-text-field
          id="sets"
          outlined
          class="form-control"
          :rules="numericRules"
          label="Sets"
          v-model.number="exercise.sets"
        >
          <v-icon slot="append" @click="exercise.sets++">
            mdi-chevron-up
          </v-icon>
          <v-icon slot="prepend" @click="exercise.sets--">
            mdi-chevron-down
          </v-icon>
        </v-text-field>
        <v-divider />
        <v-card-actions>
          <v-btn color="success" :to="{ name: 'register' }"
            ><span>Register</span></v-btn
          >
          <v-spacer />
          <v-btn color="info" type="submit"><span>Submit</span></v-btn>
        </v-card-actions>
      </v-form>
    </v-card-text>
  </v-card>
</template>

<script>
import exerciseService from "../services/ExerciseService";
import focusService from "../services/FocusService";

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
      focusRules: [
        (v) =>
          this.focusNames.includes(v) ||
          "You can only select predefined focuses!",
      ],
      focuses: [],
      pickedFocusName: "",
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
    createExercise() {
      let arrayWithFocusObj = this.focuses.filter((f) => {
        console.log(f.focusName, this.pickedFocusName.toLowerCase());
        return f.focusName === this.pickedFocusName.toLowerCase();
      });
      this.exercise.focusId = arrayWithFocusObj[0].focusId; // This should *always* work
      exerciseService
        .postExercise(this.exercise)
        .then((response) => {
          console.log(response.status);
          if (response.status === 201) {
            this.$router.push({ name: "exercises" });
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
};
</script>
