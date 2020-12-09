<template>
  <div>
    <h1>Manage Exercises</h1>
    <v-combobox
      id="focus"
      outlined
      class="form-control"
      :rules="focusRules"
      label="Focus"
      :items="focusNames"
      v-model="focusFilter"
      clearable
      @blur="filter"
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
      @click:append-outer="timeFilter += 10"
      @click:prepend="timeFilter -= 10"
      v-model.number="timeFilter"
      @blur="filter"
    />
    <exercise-list :exercises="exercises" />
  </div>
</template>

<script>
import ExerciseList from "@/components/ExerciseList";
import focusService from "../services/FocusService";
import exerciseService from "@/services/ExerciseService";

export default {
  components: { ExerciseList },
  data() {
    return {
      exercises: [],
      focusFilter: "",
      timeFilter: null,
      focuses: [],
      focusRules: [
        (v) =>
          this.focusNames.includes(v) ||
          !v ||
          "You can only select predefined focuses!",
      ],
      numericRules: [(v) => (v || 0) >= 0 || "Negative values are not allowed"],
    };
  },
  created() {
    exerciseService
      .getTrainerExercise(this.$store.state.user.username)
      .then((response) => {
        if (response.status === 200) {
          console.log(response);
          this.exercises = response.data;
        }
      });
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
  computed: {
    focusNames() {
      return this.focuses.map(
        (f) => f.focusName.charAt(0).toUpperCase() + f.focusName.slice(1) // Capitalize first letter :p
      );
    },
  },
  methods: {
    getFocusId() {
      let arrayWithFocusObj = this.focuses.filter((f) => {
        console.log(f.focusName, this.focusFilter.toLowerCase());
        return f.focusName === this.focusFilter.toLowerCase();
      });
      return arrayWithFocusObj[0].focusId;
    },
    filter() {
      let filter = [];
      if (this.focusFilter === []) {
        filter.concat({ focusId: this.focusFilter });
      }
      if (this.timeFilter) {
        filter.concat({ time: this.timeFilter });
      }
      exerciseService
        .getTrainerExercise(this.$store.state.user.username, filter)
        .then((response) => {
          console.log(response)
          if (response.status === 200) {
            console.log(response);
            this.exercises = response.data;
          }
        });
    },
  },
};
</script>

<style></style>
