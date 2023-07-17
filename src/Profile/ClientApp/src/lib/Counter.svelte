<script lang="ts">
  type WeatherForecase = {
    date: Date,
    temperatureC: number,
    temperatureF: number,
    summary?: string
  }

  let count: number = 0
  let weather: Array<WeatherForecase> = []
  const increment = async () => {
    count += 1;
    const response = await fetch("/api/blog");
    weather = await response.json();
    console.log(weather);
  }
</script>

<button on:click={increment}>
  count is {count}
</button>