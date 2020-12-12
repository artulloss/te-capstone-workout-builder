<template>
  <v-card class="container mx-auto mt-5">
    <v-card-title>
      <div class="flex-container">
        <h1 class="display-1">Manage Exercises</h1>
        <v-btn :to="{ name: 'create-exercise' }" color="primary"
          >Create Exercise</v-btn
        >
      </div>
    </v-card-title>
    <v-card-text>
      <v-select
        id="focus"
        outlined
        class="form-control"
        label="Focus"
        :items="focusNames"
        v-model="focusFilter"
        clearable
        @change="updateUrl"
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
        @click:append-outer="updateTimeFilter(10)"
        @click:prepend="updateTimeFilter(-10)"
        v-model.number="timeFilter"
        @change="updateUrl"
      />
      <exercise-list
        :exercises="exercises"
        :focuses="focuses"
        @delete-exercise="internalExercises = $event"
      />
    </v-card-text>
  </v-card>
</template>

<script>
import ExerciseList from "@/components/ExerciseList";
import focusService from "@/services/FocusService";
import exerciseService from "@/services/ExerciseService";

// The particles stretch to fix the canvas size on resize, we recall it when the page size changes
const fixParticles = (newData, oldData) => {
  setTimeout(() => {
    if (newData === oldData) return;
    let event = document.createEvent("UIEvents");
    event.initUIEvent("resize", true, false, window, 0);
    window.dispatchEvent(event);
  }, 200); // 200ms delay
};

export default {
  components: { ExerciseList },
  data() {
    return {
      internalExercises: [],
      focusFilter: "",
      timeFilter: null,
      focuses: [],
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
  watch: {
    focusFilter: fixParticles,
    timeFilter: fixParticles,
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
        .getTrainerExercises(this.$store.state.user.username, filter)
        .then((response) => {
          console.log(response);
          if (response.status === 200) {
            this.internalExercises = response.data;
            fixParticles();
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    getFilter() {
      const filter = [];
      if (this.focusFilter) {
        filter.focus = this.focusFilter;
      }
      if (this.timeFilter) {
        filter.time = this.timeFilter;
      }
      return filter;
    },
    updateUrl() {
      window.history.pushState(
        {},
        "",
        "/" + this.$route.name + exerciseService.getQueryRoute(this.getFilter())
      );
    },
    updateTimeFilter(amount) {
      let newTime;
      newTime = Number(this.timeFilter) + amount;
      this.timeFilter = newTime === 0 ? "" : newTime;
      this.updateUrl();
    },
  },
};
</script>

<style scoped>
h1 {
  text-align: center;
  padding-bottom: 0.5rem;
}
.flex-container {
  width: 100%;
  justify-content: space-between;
  flex-wrap: wrap;
}
@media only screen and (max-width: 853px) {
  .flex-container {
    justify-content: space-evenly;
  }
}
</style>
