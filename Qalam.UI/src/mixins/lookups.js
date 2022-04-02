export default {
    data: () => ({
        lookups: {
            country: null,
            educationType: null,
            educationYear: null,
            semester: null,
            subject: null,
            unit: null,
            lesson: null,
            countrySubject: null
        }
    }),

    computed: {
        countries(){
            return this.$store.getters.Countries || [];
        },
        educationTypes(){
            if(this.countries != undefined){
                var countryId = this.lookups.country;
                if(typeof(this.lookups.country) == "object"){
                    countryId = null;
                    if(this.lookups.country != undefined)
                        countryId = this.lookups.country.id
                }
                //this.lookups.educationType = null;
                var result = this.countries.find(c => c.id == countryId);
                return result && result.educationTypes ? result.educationTypes : [];
            }
            return [];
        },
        educationYears(){
            if(this.educationTypes != undefined){
                var educationTypeId = this.lookups.educationType;
                if(typeof(this.lookups.educationType) == "object"){
                    educationTypeId = null;
                    if(this.lookups.educationType != undefined)
                        educationTypeId = this.lookups.educationType.id
                }
                //this.lookups.educationYear = null;
                var result = this.educationTypes.find(c => c.id == educationTypeId);
                return result && result.educationYears ? result.educationYears : [];
            }
            return [];
        },
        semesters(){
            if(this.educationYears != undefined){
                var educationYearId = this.lookups.educationYear;
                if(typeof(this.lookups.educationYear) == "object"){
                    educationYearId = null;
                    if(this.lookups.educationYear != undefined)
                        educationYearId = this.lookups.educationYear.id
                }
                //this.lookups.semester = null;
                var result = this.educationYears.find(c => c.id == educationYearId);
                return result && result.semesters ? result.semesters : [];
            }
            return [];
        },
        subjects(){
            if(this.educationYears != undefined){
                var educationYearId = this.lookups.educationYear;
                if(typeof(this.lookups.educationYear) == "object"){
                    educationYearId = null;
                    if(this.lookups.educationYear != undefined)
                        educationYearId = this.lookups.educationYear.id
                }
                //this.lookups.subject = null;
                var result = this.educationYears.find(c => c.id == educationYearId);
                return result && result.courses ? result.courses : [];
            }
            return [];
        },   
        units(){
            if(this.subjects != undefined){
                var subjectId = this.lookups.subject;
                if(typeof(this.lookups.subject) == "object"){
                    subjectId = null;
                    if(this.lookups.subject != undefined)
                        subjectId = this.lookups.subject.id
                }
                //this.lookups.unit = null;
                var result = this.subjects.find(c => c.id == subjectId);
                return result && result.units ? result.units : [];
            }
            return [];
        },
        lessons(){
            if(this.units != undefined){
                var unitId = this.lookups.unit;
                if(typeof(this.lookups.unit) == "object"){
                    unitId = null;
                    if(this.lookups.unit != undefined)
                        unitId = this.lookups.unit.id
                }
                //this.lookups.lesson = null;
                var result = this.units.find(c => c.id == unitId);
                return result && result.lessons ? result.lessons : [];
            }
            return [];
        },
        countrySubjects(){
            if(this.countries != undefined){
                var countryId = this.lookups.country;
                if(typeof(this.lookups.country) == "object"){
                    countryId = null;
                    if(this.lookups.country != undefined)
                        countryId = this.lookups.country.id
                }
                this.lookups.countrySubject = null;
                var result = this.countries.find(c => c.id == countryId);
                return result && result.subjects ? result.subjects : [];
            }
            return [];
        },
        subjectLessons(){
            this.lookups.lesson = null;
            if(this.units != undefined && this.units.length > 0){
                return this.units.map(p => { return p.lessons; })
                    .reduce((a, b) => { return a.concat(b); });
            }
            return [];
        }
    }
}