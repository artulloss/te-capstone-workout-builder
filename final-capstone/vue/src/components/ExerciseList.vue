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
              <p :class="getExerciseClass(e)">
                {{ getFocusName(e.focusId) }}
              </p>
            </li>
            <li>
              <b>Time: </b>
              <p :class="getExerciseClass(e)">
                {{ secondsToMinutes(e.time) }}
              </p>
            </li>
            <li v-if="e.weight !== null">
              <b>Weight: </b>
              <p :class="getExerciseClass(e)">{{ e.weight }} lbs</p>
            </li>
            <li v-if="e.repetitions !== null">
              <b>Repetitions: </b>
              <p :class="getExerciseClass(e)">
                {{ e.repetitions }}
              </p>
            </li>
            <li v-if="e.sets !== null">
              <b>Sets: </b>
              <p :class="getExerciseClass(e)">
                {{ e.sets }}
              </p>
            </li>
          </ul>
          <edit-exercise
            v-if="exerciseEditable[e.exerciseId]"
            :exercise="e"
            :focuses="focuses"
            @edit-exercise="$emit('edit-exercise', $event)"
          />
          <div class="flex-container flex-center">
            <v-btn color="primary" @click="editExercise(e)">
              <span>{{
                exerciseEditable[e.exerciseId] ? "Save" : "Edit"
              }}</span>
              <!--change color maybe?-->
            </v-btn>
            <v-btn color="red" dark @click="deleteExercise(e)">
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
import exerciseService from "@/services/ExerciseService";
import EditExercise from "./EditExercise.vue";
import utilities from "@/utilities";

export default {
  name: "exercise-list",
  components: { EditExercise },
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
        utilities.capitalizeFirstLetter(f.focusName)
      );
    },
  },
  methods: {
    secondsToMinutes: utilities.secondsToMinutes,
    getFocusName(id) {
      let arrayWithFocusObj = this.focuses.filter((f) => {
        return f.focusId === id;
      });
      return utilities.capitalizeFirstLetter(
        (arrayWithFocusObj[0] || { focusName: undefined }).focusName
      );
    },
    editExercise(exercise) {
      this.toggleExerciseVisible(exercise.exerciseId);
      if (!this.exerciseEditable[exercise.exerciseId]) {
        exerciseService.updateExercise(exercise).catch((e) => {
          console.log(e);
        });
      }
      return exercise;
    },

    deleteExercise({ exerciseName, exerciseId }) {
      if (!confirm(`Are you sure you want to delete ${exerciseName}`)) return;
      const newExercises = this.exercises.filter((e) => {
        return exerciseId !== e.exerciseId;
      });
      this.$emit("delete-exercise", newExercises);
      exerciseService.deleteExercise(exerciseId);
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
