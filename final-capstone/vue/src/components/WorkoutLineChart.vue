<script>
import { Line } from "vue-chartjs";
export default {
  extends: Line,
  name: "workout-line-chart",
  props: {
    chartdata: Object,
    options: Object,
  },
  mounted() {
    this.renderChart(this.chartdata, this.options);
  },
};
</script>
