<template>
  <v-card class="container mx-auto mt-5">
    <v-card-title>
      <div class="flex-container">
        <h1 class="display-1">My Exercises</h1>
        <v-btn :to="{ name: 'generate-workout' }" color="primary"
          >Generate Workout</v-btn
        >
      </div>
    </v-card-title>
    <v-card-text>
      <v-expansion-panels multiple>
        <v-expansion-panel v-for="e in exercises" :key="e.exerciseId">
          <v-expansion-panel-header expand-icon="mdi-menu-down">
            <b>{{ e.exerciseName }}</b>
          </v-expansion-panel-header>
          <v-expansion-panel-content>
            <p>
              {{ e.description }}
            </p>
            <ul class="flex-container">
              <li>
                <b>Focus: </b>
                <p>
                  {{ getFocusName(e.focusId) }}
                </p>
              </li>
              <li>
                <b>Time: </b>
                <p>
                  {{ e.time }}
                </p>
              </li>
              <li>
                <b>Repetitions: </b>
                <p v-if="e.repetitions !== null">
                  {{ e.repetitions }}
                </p>
              </li>
              <li>
                <b>Sets: </b>
                <p v-if="e.repetitions !== null">
                  {{ e.sets }}
                </p>
              </li>
              <li>
                <b>Weight: </b>
                <p v-if="e.repetitions !== null">
                  {{ e.weight }}
                </p>
              </li>
            </ul>
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-expansion-panels>
    </v-card-text>
  </v-card>
</template>

<script>
import focusService from "@/services/FocusService";
import exerciseService from "@/services/ExerciseService";

export default {
  created() {
    this.getFocuses();
    this.getExercises();
  },
  data() {
    return {
      exercises: [],
      focuses: [],
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
    capitalizeFirstLetter(string) {
      string = string + "";
      return string.charAt(0).toUpperCase() + string.slice(1);
    },
    getFocusName(id) {
      let arrayWithFocusObj = this.focuses.filter((f) => {
        return f.focusId === id;
      });
      return this.capitalizeFirstLetter(
        (arrayWithFocusObj[0] || { focusName: undefined }).focusName
      );
    },

    getExercises() {
      exerciseService
        .getUserExercises(this.$store.state.user.username)
        .then((response) => {
          console.log(response);
          if (response.status === 200) {
            this.exercises = response.data;
            //fixParticles();
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
};
</script>

<style scoped>
.flex-container {
  width: 100%;
  justify-content: space-between;
  flex-wrap: wrap;
}

.flex-center {
  justify-content: center;
}

.flex-center >>> * {
  margin: 0 0.5rem;
}

li {
  list-style: none;
}

ul {
  padding: 1rem 0;
}

p {
  display: inline;
}
</style>
