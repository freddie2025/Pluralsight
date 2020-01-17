package module1

import (
	"go/ast"
	"go/parser"
	"go/token"
	"io/ioutil"
	"log"
	"path"
	"reflect"
	"runtime"
	"strings"
	"testing"
)

func TestBudgetStructIsDefined(t *testing.T) {
	didFindAStruct, didFindTheStruct := checkStruct("Budget")

	if !didFindAStruct || !didFindTheStruct {
		t.Error("Did not define a struct named Budget")
	}
}

func TestItemStructIsDefined(t *testing.T) {
	didFindAStruct, didFindTheStruct := checkStruct("Item")

	if !didFindAStruct || !didFindTheStruct {
		t.Error("Did not define a struct named Item")
	}
}

func TestBudgetHasProperties(t *testing.T) {
	if !checkStructProperties("Budget", "Max", "float32") {
		t.Error("Did not define `Max` field in `Budget` with the proper type")
	}
	if !checkStructProperties("Budget", "Items", "[]Item") {
		t.Error("Did not define `Items` field in `Budget` with the proper type")
	}
}

func TestItemHasProperties(t *testing.T) {
	if !checkStructProperties("Item", "Description", "string") {
		t.Error("Did not define `Description` field in `Item` with the proper type")
	}
	if !checkStructProperties("Item", "Price", "float32") {
		t.Error("Did not define `Price` field in `Item` with the proper type")
	}
}

func checkStructProperties(structName, fieldName, fieldType string) bool {
	var targetStruct *ast.TypeSpec

	_, currentFile, _, _ := runtime.Caller(1)

	src, err := ioutil.ReadFile(path.Join(path.Dir(currentFile), "budget_1.go"))
	if err != nil {
		log.Fatal(err)
	}

	fset := token.NewFileSet()
	f, err := parser.ParseFile(fset, "", src, 0)
	if err != nil {
		panic(err)
	}

	for _, decl := range f.Decls {
		if reflect.TypeOf(decl).String() == "*ast.GenDecl" {
			genDecl := decl.(*ast.GenDecl)
			for _, spec := range genDecl.Specs {
				if reflect.TypeOf(spec).String() == "*ast.TypeSpec" {
					typeSpec := spec.(*ast.TypeSpec)
					if reflect.TypeOf(typeSpec.Type).String() == "*ast.StructType" {
						if typeSpec.Name.Name == structName {
							targetStruct = typeSpec
							break
						}
					}
				}
			}
		}
	}

	if targetStruct == nil {
		return false
	}

	targetStructType := targetStruct.Type.(*ast.StructType)
	//ast.Print(fset, targetStructType)
	for _, field := range targetStructType.Fields.List {
		for _, name := range field.Names {
			if name.Name == fieldName {
				switch reflect.TypeOf(field.Type).String() {
				case "*ast.Ident":
					fType := field.Type.(*ast.Ident)
					return fType.Name == fieldType

				case "*ast.ArrayType":
					if !strings.Contains(fieldType, "[]") {
						return false
					}
					aType := field.Type.(*ast.ArrayType)
					elt := aType.Elt.(*ast.Ident)
					return elt.Name == strings.ReplaceAll(fieldType, "[]", "")
				}
			}
		}
	}
	return false
}

func checkStruct(structName string) (bool, bool) {
	var foundAStruct bool
	var foundBudgetStruct bool

	_, currentFile, _, _ := runtime.Caller(1)

	src, err := ioutil.ReadFile(path.Join(path.Dir(currentFile), "budget_1.go"))
	if err != nil {
		log.Fatal(err)
	}

	fset := token.NewFileSet()
	f, err := parser.ParseFile(fset, "", src, 0)
	if err != nil {
		panic(err)
	}

	for _, decl := range f.Decls {
		if reflect.TypeOf(decl).String() == "*ast.GenDecl" {
			genDecl := decl.(*ast.GenDecl)
			for _, spec := range genDecl.Specs {
				if reflect.TypeOf(spec).String() == "*ast.TypeSpec" {
					typeSpec := spec.(*ast.TypeSpec)
					if reflect.TypeOf(typeSpec.Type).String() == "*ast.StructType" {
						foundAStruct = true
						if typeSpec.Name.Name == structName {
							foundBudgetStruct = true
						}
					}
				}
			}
		}
	}
	return foundAStruct, foundBudgetStruct
}
