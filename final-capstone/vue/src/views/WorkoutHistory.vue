<template>
  <v-card class="container mx-auto mt-5">
    <v-card-title>
      <div class="flex-container">
        <h1 class="display-1">Workout History</h1>
        <v-btn :to="{ name: 'exercises' }" color="primary">Exercises</v-btn>
      </div>
    </v-card-title>
    <v-card-text>
      <workout-line-chart :chartData="chartData" :options="chartOptions" />
    </v-card-text>
  </v-card>
</template>

<script>
import WorkoutLineChart from "@/components/WorkoutLineChart.vue";
import axios from "axios";
export default {
  name: "workout-history",
  data() {
    return {
      chartData: {
        labels: [],
        datasets: [
          {
            label: "Minutes",
            backgroundColor: "#f87979",
            data: [],
          },
        ],
      },
      chartOptions: {
        responsive: true,
        maintainAspectRatio: false,
      },
    };
  },
  created() {
    axios.get(`/workoutHistory/${this.$store.state.user.userId}`).then((response) => {
      console.log({response});
      if (response.status === 200) {
        this.chartData.datasets.data = response.data.map((wH) => wH.time / 60);
        this.chartData.labels = response.data.map((wH) => wH.date);
      }
    });
  },
  components: { WorkoutLineChart },
};
</script>

<style scoped>
.flex-container {
  justify-content: space-between;
  width: 100%;
}
</style>
