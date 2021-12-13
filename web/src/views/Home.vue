<template>
  <div class="home">
    <div class="titlebox">
      <img class="logo" alt="Genk Logo" src="../assets/logo.png" width="150"/>
      <div id="title">
        <h1>RACING GENK</h1>
        <p>Abonnees per gemeente met minstens <b>100</b> abonnees</p>
      </div>
    </div>
    <div class="filter-buttons">
      <input type="radio" id="town" value="town" v-model="filter" />
      <label for="town">Gemeente</label>
      <input type="radio" id="postalCode" value="postalCode" v-model="filter" />
      <label for="postalCode">Postcode</label>
    </div>
    <div id="content">
      <svg id="canvas" :width=width height=1000>

        <g v-for="(d, index) in data" :key="d" :transform="`translate(${calculateXPosition(index)}, ${calculateYPosition(index)})`" class="data-circle" v-on:mouseover="selectCircle(d)">
          <circle :r="calculateRadius(d)">
          </circle>
          <circle class="circle-overlay" :r="calculateRadius(d)">
          </circle>
          <text y=75 class="label">
            {{this.filter == 'town' ? d.city : d.postcode}}
          </text>
        </g>
      </svg>

      <div v-for="(d, index) in data" :key="d">
          <Card :show="this.selectedData == d" :x="calculateXPosition(index)" :y="calculateYPosition(index)" :title="this.filter == 'town' ? d.city : d.postcode.toString()" :content="d.amount.toString()"/>
      </div>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src
import * as d3 from "d3";
import Card from '../components/Card.vue';

export default {
  name: 'Home',
  components: {
    Card
  },
  data: function() {
    return {
      selectedData: Object,
      data: Object,
      maxRadius: 50,
      rowLength: 9,
      width: 1800,
      filter: 'town'
    }
  },
  async mounted() 
  {
    //Hide the card
    d3.select('.card').style("visibility", "hidden");


    //Load the data
    this.data = await d3.csv('abonnees_genk.csv', this.typeConversion);
    this.data = this.dataLoaded(this.data);
    this.data = this.prepareData(this.data);
    //this.data.sort((a, b) => { return b.amount - a.amount} );

    console.log(this.data);
  },
  methods: {
    selectCircle: function(d)
    {
      this.selectedData = d;
    },
    typeConversion: function(d) {
      return {
        postcode: +d.Postcode,
        city: d.City
      }
    },
    dataLoaded : function(d)
    {
      const filtered = d.filter(r => {
          return r.postcode && r.postcode > 999 && r.postcode < 10000;
      });
      console.log(filtered);
      return filtered;
    },
    prepareData : function(data)
    {
      const dataMap = d3.rollup(
          data,
          r => d3.count(r, x => x.postcode),
          d => d.postcode
      )

      console.log(dataMap);

      const dataArray = Array.from(dataMap, d => ({ postcode: d[0], amount: d[1], city: data.find(e => e.postcode == d[0]).city.toUpperCase() }));
      const filteredDataArray = dataArray.filter(r => {
        return r.amount > 99;
      })
      return filteredDataArray;
    },
    calculateRadius: function(da)
    {
      const extents = d3.extent(this.data, d => d.amount);
      const t = da.amount / extents[1];
      return t * this.maxRadius;
    },
    calculateXPosition: function(i)
    {
      const w = this.width - 75 * 2;
      const div = w / this.rowLength;
      return 75 + div/2 + (i % this.rowLength) * div;
    },
    calculateYPosition: function(i)
    {
      return 50+ Math.floor(i/ this.rowLength) * 150;
    }
  }
}
</script>

<style >

.titlebox {
  display: flex;
  height: 200px;
}

#title h1 {
  margin-bottom: 0px;
}

#title p {
  margin-top : 0px;
}

img {
  display: block;
  width: auto;
  height: auto;
}

.filter-buttons {
  margin-top: 50px;
  margin-bottom: 20px;
  display:flex;
  justify-content: right;
}

#title {
  margin-left: 50px;
  padding-top: 50px;
}

h1 {
  text-align: left;
}

#content {
  position: relative;
}

.circle-overlay {
  fill: var(--dark-blue-color);
  transform: scale(0);
  transition: ease-out 300ms;
}

.data-circle:hover .circle-overlay {
  transform: scale(1);
}

circle {
  fill: var(--light-blue-color);
}

.label {
  fill: var(--light-blue-color);
}

text {
  text-anchor: middle;
}

</style>
