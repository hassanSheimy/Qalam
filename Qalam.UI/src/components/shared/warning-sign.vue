<template>
    <div class="content left">
        <div class="browser-bar">
            <span class="close button"></span>
            <span class="close min button"></span>
            <span class="close max button"></span>
        </div>
        <div class="text">
            <div class="text-center ma-4">
                <v-icon :size="size" :color="color">{{icon}}</v-icon>
            </div>
            <div class="text-center ma-4">
                <slot />
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "WarningSign",

    props: {
        icon: {
            type: String,
            default: "fas fa-exclamation-triangle"
        },
        size: {
            type: String | Number,
            default: "50"
        },
        color: {
            type: String,
            default: "#fc635d"
        }
    },
}
</script>

<style scoped>
body {
    overflow: hidden;
    font-family: "Lucida Console", Monaco, monospace;
    line-height: 1.5;
    min-height: 100vh;
    color: #fff
}

a {
    color: #fff;
    text-decoration: none;
    border-bottom: solid 2px;
}
span {
    color: #fff;
}

.content {
    width: 500px;
    height: 300px;
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    -webkit-transform: translate(-50%, -50%);
    -ms-transform: translate(-50%, -50%);
    background-color: #333;
    padding: 45px 20px 20px;
    box-sizing: border-box;
    box-shadow: 0 0 25px rgba(0, 0, 0, 0.5);
    border-radius: 5px 5px 0 0;
}

.browser-bar {
    background: #f9f9f3;
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    padding: 5px;
    overflow: hidden;
    border-radius: 5px 5px 0 0;
}

.button {
    display: inline-block;
    border-radius: 50%;
    width: 15px;
    height: 15px;
    margin-right: 5px;
}

.close {
    background: #fc635d;
}

.min {
    background: #fdbc40;
}

.max {
    background: #34c84a;
}

@media (max-width: 640px) {
    .content {
        width: 95%;
        height: 400px;
    }
}
</style>