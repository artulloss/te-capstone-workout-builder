<template>
  <div>
    <v-expansion-panels>
      <v-expansion-panel v-for="e in exercises" :key="e.exerciseId">
        <v-expansion-panel-header expand-icon="mdi-menu-down">
          <b>{{ e.exerciseName }}</b>
        </v-expansion-panel-header>
        <v-expansion-panel-content>
          <p contentEditable="">
            {{ e.description }}
          </p>
          <div class="flex-container">
            <p><b>Focus:</b> {{ getFocusName(e.focusId) }}</p>
            <p><b>Time:</b> {{ e.time }}</p>
            <p v-if="e.repetitions"><b>Repetitions:</b> {{ e.repetitions }}</p>
            <p v-if="e.sets"><b>Sets:</b> {{ e.sets }}</p>
            <p v-if="e.weight"><b>Weight:</b> {{ e.weight }}</p>
          </div>
          <div class="flex-container flex-center">
            <v-btn color="primary">
              <span>Edit</span>
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
export default {
  name: "exercise-list",
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
      console.log("FOCUSES", this.focuses);
      let arrayWithFocusObj = this.focuses.filter((f) => {
        return f.focusId === id;
      });
      return this.capitalizeFirstLetter(
        (arrayWithFocusObj[0] || { focusName: undefined }).focusName
      );
    },
    editExercise(exercise) {
      return exercise;
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
</style>
