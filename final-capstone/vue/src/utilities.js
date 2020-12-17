export default {
  numericRules: [(v) => (v || 0) >= 0 || "Negative values are not allowed"],
  //updateNumericValue(value, amount) {}
  secondsToMinutes(seconds) {
    if (!Number(seconds) || seconds < 0) return "Invalid";
    const minutes = Math.floor(seconds / 60);
    seconds = seconds % 60;
    const minuteString = minutes === 1 ? "minute" : "minutes";
    const secondString = seconds === 1 ? "second" : "seconds";
    if (minutes > 0 && seconds > 0) {
      return `${minutes} ${minuteString} and ${seconds} ${secondString}`;
    }
    if (minutes > 0) {
      return `${minutes} ${minuteString}`;
    }
    if (seconds > 0) {
      return `${seconds} ${secondString}`;
    }
  },
  requiredNumericRules() {
    return (
      this.numericRules.concat((v) => (v || "").length !== 0) ||
      "This is a required field"
    );
  },
  capitalizeFirstLetter(string) {
    string = string + "";
    return string.charAt(0).toUpperCase() + string.slice(1);
  },
};
