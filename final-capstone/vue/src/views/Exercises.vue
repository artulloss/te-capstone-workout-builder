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
            <ul class="flex-container space-evenly">
              <li>
                <b>Focus: </b>
                <p>
                  {{ getFocusName(e.focusId) }}
                </p>
              </li>
              <li>
                <b>Time: </b>
                <p>
                  {{ secondsToMinutes(e.time) }}
                </p>
              </li>
              <li v-if="e.repetitions !== null">
                <b>Repetitions: </b>
                <p>
                  {{ e.repetitions }}
                </p>
              </li>
              <li v-if="e.repetitions !== null">
                <b>Sets: </b>
                <p>
                  {{ e.sets }}
                </p>
              </li>
              <li v-if="e.repetitions !== null">
                <b>Weight: </b>
                <p>
                  {{ e.weight }}
                </p>
              </li>
            </ul>
            <div class="flex-container flex-center">
              <v-btn color="primary">Mark Complete</v-btn>
            </div>
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-expansion-panels>
      <p v-if="!exercises.length">
        This is where your workouts will show up.
      </p>
      <v-card-actions
        ><div class="flex-container flex-center">
          <v-btn color="primary">Log Workout</v-btn>
        </div>
      </v-card-actions>
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
    secondsToMinutes(seconds) {
      const minutes = Math.floor(seconds / 60);
      seconds = seconds % 60;
      if (minutes > 0 && seconds > 0) {
        return `${minutes} minutes and ${seconds} seconds`;
      }
      if (minutes > 0) {
        return minutes + " minutes";
      }
      if (seconds > 0) {
        return seconds + " seconds";
      }
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

.space-evenly {
  justify-content: space-evenly;
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
