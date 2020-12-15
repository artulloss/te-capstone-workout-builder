<template>
  <v-card class="container mx-auto mt-5">
    <v-card-title>
      <div class="flex-container">
        <h1 class="display-1">Workout History</h1>
      </div>
    </v-card-title>
    <v-card-text>
      <workout-line-chart :chartdata="chartData" :options="chartOptions" />
    </v-card-text>
  </v-card>
</template>

<script>
import WorkoutLineChart from "@/components/WorkoutLineChart.vue";
export default {
  name: "workout-history",
  data() {
    return {
      chartdata: {
        labels: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"],
        datasets: [
          {
            label: "Minutes",
            backgroundColor: "#f87979",
            data: [40, 20, 0, 30, 60],
          },
        ],
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
      },
    };
  },
  components: { WorkoutLineChart },
  mounted() {
    this.renderChart(this.chartdata, this.options);
  },
};
</script>

<style></style>
