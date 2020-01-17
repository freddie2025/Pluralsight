package main

import (
	"fmt"
	m2 "personal-budget/module2"
	"time"
)

var months = []time.Month{
	time.January,
	time.February,
	time.March,
	time.April,
	time.May,
	time.June,
	time.July,
	time.August,
	time.September,
	time.October,
	time.November,
	time.December,
	time.January,
}

func main() {
	bu, _ := m2.CreateBudget(time.January, 1000)
	bu.AddItem("bananas", 10.0)

	fmt.Println("Items in January:", len(bu.Items))
	fmt.Printf("Current cost for January: $%.2f \n", bu.CurrentCost())

	m2.CreateBudget(time.February, 1000)

	bu2 := m2.GetBudget(time.February)
	bu2.AddItem("bananas", 10.0)
	bu2.AddItem("coffee", 3.99)
	bu2.AddItem("gym", 50.0)
	bu2.RemoveItem("coffee")
	fmt.Println("Items in February:", len(bu2.Items))
	fmt.Printf("Current cost for February: $%.2f \n", bu2.CurrentCost())

	fmt.Println("Resetting Budget Report...")
	m2.InitializeReport()

	for _, month := range months {
		_, err := m2.CreateBudget(month, 100.00)
		if err == nil {
			fmt.Println("Budget created for", month)
		} else {
			fmt.Println("Error creating budget:", err)
		}
	}

	_, err := m2.CreateBudget(time.December, 100.00)
	fmt.Println(err)
}
