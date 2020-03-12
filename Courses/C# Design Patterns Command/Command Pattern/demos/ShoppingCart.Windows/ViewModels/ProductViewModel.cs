using ShoppingCart.Business.Commands;
using ShoppingCart.Business.Models;
using ShoppingCart.Business.Repositories;

namespace ShoppingCart.Windows.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public System.Windows.Input.ICommand AddToCartCommand { get; private set; }
        public System.Windows.Input.ICommand IncreaseQuantityCommand { get; private set; }
        public System.Windows.Input.ICommand DecreaseQuantityCommand { get; private set; }
        public System.Windows.Input.ICommand RemoveFromCartCommand { get; private set; }
        
        public ProductViewModel(ShoppingCartViewModel shoppingCartViewModel,
            IShoppingCartRepository shoppingCartRepository,
            IProductRepository productRepository,
            Product product,
            int quantity = 0)
        {
            Product = product;
            Quantity = quantity;

            var addToCartCommand = 
                new AddToCartCommand(shoppingCartRepository, productRepository, product);

            var increaseQuantityCommand = 
                new ChangeQuantityCommand(ChangeQuantityCommand.Operation.Increase, 
                    shoppingCartRepository, 
                    productRepository, 
                    product);

            var decreaseQuantityCommand = 
                new ChangeQuantityCommand(ChangeQuantityCommand.Operation.Decrease, 
                    shoppingCartRepository,
                    productRepository, 
                    product);

            var removeFromCartCommand = 
                new RemoveFromCartCommand(shoppingCartRepository, 
                    productRepository, 
                    product);

            AddToCartCommand = new RelayCommand(
                    execute: () => {
                        addToCartCommand.Execute();
                        shoppingCartViewModel.Refresh();
                    },
                    canExecute: () => addToCartCommand.CanExecute());

            IncreaseQuantityCommand = new RelayCommand(
                execute: () => {
                    increaseQuantityCommand.Execute();
                    shoppingCartViewModel.Refresh();
                },
                canExecute: () => increaseQuantityCommand.CanExecute());

            DecreaseQuantityCommand = new RelayCommand(
                execute: () => {
                    decreaseQuantityCommand.Execute();
                    shoppingCartViewModel.Refresh();
                },
                canExecute: () => decreaseQuantityCommand.CanExecute());

            RemoveFromCartCommand = new RelayCommand(
                execute: () => {
                    removeFromCartCommand.Execute();
                    shoppingCartViewModel.Refresh();
                },
                canExecute: () => removeFromCartCommand.CanExecute());
        }
    }
}
