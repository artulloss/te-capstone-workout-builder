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
      validate-on-blur
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
      internalExercises: [],
      focusFilter: "",
      timeFilter: null,
      focuses: [],
      focusRules: [
        (v) =>
          !v ||
          this.focusNames
            .map((f) => f.toLowerCase()) // Case insensitive matching
            .includes(v.toLowerCase()) ||
          "You can only select predefined focuses!",
      ],
      numericRules: [(v) => (v || 0) >= 0 || "Negative values are not allowed"],
    };
  },
  created() {
    this.getFocuses();
    this.getExercises(); // Keep it simple and not use the filter params
    // Next we parse the route query
    let query = this.$route.query;
    console.log(query);
    for (const prop in query) {
      if (prop === "focus") {
        this.focusFilter = query[prop];
      } else if (prop === "time") {
        this.timeFilter = Number(query[prop]); // Query props are strings
      }
    }
  },
  computed: {
    focusNames() {
      return this.focuses.map(
        (f) => f.focusName.charAt(0).toUpperCase() + f.focusName.slice(1) // Capitalize first letter :p
      );
    },
    exercises() {
      return this.internalExercises.filter((exercise) => {
        return (
          ((!this.focusFilter && this.focusFilter !== 0) || // 0 is falsey
            this.getFocusId(this.focusFilter) === exercise.focusId) &&
          ((!this.timeFilter && this.timeFilter !== 0) || // 0 is falsey
            this.timeFilter === exercise.time)
        );
      });
    },
  },
  methods: {
    getFocusId() {
      let arrayWithFocusObj = this.focuses.filter((f) => {
        return f.focusName === (this.focusFilter || "").toLowerCase();
      });
      return (arrayWithFocusObj[0] || { focusId: undefined }).focusId;
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
    getExercises(filter = {}) {
      exerciseService
        .getTrainerExercise(this.$store.state.user.username, filter)
        .then((response) => {
          console.log(response);
          if (response.status === 200) {
            this.internalExercises = response.data;
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
};
</script>

<style></style>
