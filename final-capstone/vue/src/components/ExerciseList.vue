<template>
  <div>
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
              <p
                :class="getExerciseClass(e)"
                :contentEditable="exerciseEditable[e.exerciseId]"
              >
                {{ getFocusName(e.focusId) }}
              </p>
            </li>
            <li>
              <b>Time: </b>
              <p
                :class="getExerciseClass(e)"
                :contentEditable="exerciseEditable[e.exerciseId]"
              >
                {{ e.time }}
              </p>
            </li>
            <li>
              <b>Repetitions: </b>
              <p
                v-if="e.repetitions !== null"
                :class="getExerciseClass(e)"
                :contentEditable="exerciseEditable[e.exerciseId]"
              >
                {{ e.repetitions }}
              </p>
            </li>
            <li>
              <b>Sets: </b>
              <p
                v-if="e.repetitions !== null"
                :class="getExerciseClass(e)"
                :contentEditable="exerciseEditable[e.exerciseId]"
              >
                {{ e.sets }}
              </p>
            </li>
            <li>
              <b>Weight: </b>
              <p
                v-if="e.repetitions !== null"
                :class="getExerciseClass(e)"
                :contentEditable="exerciseEditable[e.exerciseId]"
              >
                {{ e.weight }}
              </p>
            </li>
          </ul>
          <div class="flex-container flex-center">
            <v-btn color="primary" @click="editExercise(e)">
              <span>{{
                exerciseEditable[e.exerciseId] ? "Save" : "Edit"
              }}</span>
              <!--change color maybe?-->
            </v-btn>
            <v-btn color="red" dark>
              <span>Delete</span>
            </v-btn>
          </div>
        </v-expansion-panel-content>
      </v-expansion-panel>
    </v-expansion-panels>
  </div>
</template>

<script>
import Vue from "vue";
//import exerciseService from "@/services/ExerciseService";

export default {
  name: "exercise-list",
  data() {
    return {
      exerciseEditable: [],
    };
  },
  props: {
    exercises: Array,
    focuses: Array,
  },
  computed: {
    focusNames() {
      return this.props.focuses.map((f) =>
        this.capitalizeFirstLetter(f.focusName)
      );
    },
  },
  methods: {
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

    editExercise(exercise) {
      console.log(document.querySelectorAll(this.getExerciseClass(exercise)));
      this.toggleExerciseVisible(exercise.exerciseId);
      if (!this.exerciseEditable[exercise.exerciseId]) {
        console.log("Adam");
        //exerciseService.updateExercise(exercise);
      }

      return exercise;
    },

    getExerciseClass({ exerciseId }) {
      return "exercise-" + exerciseId;
    },

    toggleExerciseVisible(exerciseId) {
      Vue.set(
        this.exerciseEditable,
        exerciseId,
        !this.exerciseEditable[exerciseId]
      );
    },
  },
};
</script>

<style scoped>
.flex-container {
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

p[contentEditable] {
  font-size: 30;
}
</style>
