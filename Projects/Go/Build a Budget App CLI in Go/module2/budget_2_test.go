package module2

import (
	"testing"
	"time"
)

func TestAddValidItemReturnsNoError(t *testing.T) {
	setup()
	newBudget := Budget{Max: 1000}
	itemsBefore := len(newBudget.Items)
	err := newBudget.AddItem("coffee", 5.99)
	if err != nil {
		t.Error("Returned error when adding item")
	}
	if len(newBudget.Items) != (itemsBefore + 1) {
		t.Error("Did not add item")
	}
}

func TestAddItemOverBudgetReturnsError(t *testing.T) {
	setup()
	var err error

	newBudget := Budget{Max: 100}

	err = newBudget.AddItem("lots of coffee", 80.00)
	if err != nil {
		t.Error("Returned error when adding item")
	}
	err = newBudget.AddItem("lots of bananas", 30.00)
	if err != errDoesNotFitBudget {
		t.Error("Did not check for max budget")
	}
}

func TestRemoveItem(t *testing.T) {
	setup()
	newBudget := Budget{Max: 1000}

	newBudget.AddItem("coffee", 5.99)
	newBudget.AddItem("bananas", 4.99)
	newBudget.AddItem("gym", 45.00)

	var hasCoffee bool
	for i := range newBudget.Items {
		if newBudget.Items[i].Description == "coffee" {
			hasCoffee = true
		}
	}
	if !hasCoffee {
		t.Error("Error adding new item")
	}

	hasCoffee = false
	newBudget.RemoveItem("coffee")
	for i := range newBudget.Items {
		if newBudget.Items[i].Description == "coffee" {
			hasCoffee = true
		}
	}
	if hasCoffee {
		t.Error("Did not remove item by description")
	}
}

func TestCreateValidBudgetAddsToReport(t *testing.T) {
	setup()
	before := len(report)
	_, err := CreateBudget(time.January, 1000)
	after := len(report)
	if before == after || err != nil {
		t.Error("Did not create a valid budget")
	}
}

func TestCreateInvalidBudgetDoesNotAddToReport(t *testing.T) {
	setup()
	_, err := CreateBudget(time.January, 1000)
	if err != nil {
		t.Error("Did not create valid budget: ")
	}
	_, err = CreateBudget(time.January, 2000)
	if err != errDuplicateEntry {
		t.Error("Did not return proper error")
	}

	_, err = CreateBudget(time.February, 2000)
	if err != nil {
		t.Error("Did not add valid budget to report")
	}
}

func TestCreateBudgetReturnsErrorWhenFull(t *testing.T) {
	setup()

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
	}

	for _, month := range months {
		CreateBudget(month, 100.00)
	}

	_, err := CreateBudget(time.February, 100.00)
	if err != errReportIsFull {
		t.Error("Did not return proper error")
	}

}

func TestGetBudgetReturnsCorrectBudget(t *testing.T) {
	setup()
	var err error

	_, err = CreateBudget(time.January, 1000.00)
	if err != nil {
		t.Error("Did not create a valid budget")
	}
	_, err = CreateBudget(time.February, 1500.00)
	if err != nil {
		t.Error("Did not create a valid budget")
	}
	_, err = CreateBudget(time.March, 500.00)
	if err != nil {
		t.Error("Did not create a valid budget")
	}

	februarybudget := GetBudget(time.February)

	if februarybudget == nil || februarybudget.Max != 1500.00 {
		t.Error("Did not get correct budget", februarybudget)
	}
}

func setup() {
	InitializeReport()
}
