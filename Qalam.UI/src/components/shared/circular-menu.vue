<template>
    <div class="circular-menu-container" :class="{right: position !== 'left'}">
        <button class="circular-menu-button primary" v-on:click="Open">
            <i class="fas fa-tools" v-if="!opened"></i>
            <i class="fas fa-times" v-if="opened"></i>
        </button>
        <div class="menu-container" :class="{opened: opened}" ref="circular-menu-container-ref">
            <button v-for="(button, index) in buttons" :key="'circular-menu-button-' + index" 
            @click="Click(button.callback)"
            class="circular-menu-button primary" style="transform: translate(0px, 0px) scale(0);">
                <i :class="button.icon" aria-hidden="true" />
            </button>
        </div>
    </div>
</template>

<script>
export default {
    name: "CircularMenu",

    props: {
        distance: {
            type: Number,
            default: 100        
        },
        position: {
            type: String,
            default: 'left'
        },
        scale: {
            type: Number,
            default: 1
        },
        buttons: {
            type: Array,
            required: true
        }
    },

    data: () => ({
        opened: false
    }),
    
    methods: {
        CalcRotation: function(index) {
            if (this.buttons.length == 1) {
                return 45;
            }
            return ( 90 / (this.buttons.length-1) ) * index;
        },
        Open: function() {
            this.opened = !this.opened;
            
            this.$refs['circular-menu-container-ref'].childNodes.forEach((item, index) => {
                let angle = this.CalcRotation(index) * Math.PI/180;
                angle *= this.position === 'left' ? -1 : 1;
                const x = -Math.sin(angle) * this.distance,
                      y = this.distance*Math.cos(angle);
                
                item.style = `transform: translate(${x}px,-${y}px) scale(${this.scale})`;
                if (!this.opened) {
                    item.style = `transform: translate(0,0) scale(0)`;
                }
            });
        },
        Click(callback){
            callback();
            this.Open();
        }
    },
}
</script>

<style scoped>
.circular-menu-button {
    z-index: 100;
    -webkit-transition: 0.3s cubic-bezier(0, 0, 0.2, 1);
    -moz-transition: 0.3s cubic-bezier(0, 0, 0.2, 1);
    -o-transition: 0.3s cubic-bezier(0, 0, 0.2, 1);
    transition: 0.3s cubic-bezier(0, 0, 0.2, 1);
    display: flex;
    align-items: center;
    justify-content: center;
    cursor: pointer;
    border-style: none;
    outline: 0;
    height: 64px;
    width: 64px;
    background-color: #546e7a;
    border-radius: 100%;
    box-shadow: 0 1px 3px rgba(0,0,0,0.12), 0 1px 2px rgba(0,0,0,0.24);
}
.circular-menu-button:active {
    box-shadow: 0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23);
}
.circular-menu-button i {
    color: #fff;
    font-size: 30px;
}
.circular-menu-container {
    z-index: 100;
    position: fixed;
    top: auto;
    left: 16px;
    bottom: 16px;
}
.circular-menu-container.right {
    right: 16px;
    left: initial;
}
.circular-menu-container>.menu-container > * {
    transition: 0.3s cubic-bezier(0, 0, 0.2, 1);
    position: absolute;
    z-index: -1;
    bottom: 0;
}

</style>